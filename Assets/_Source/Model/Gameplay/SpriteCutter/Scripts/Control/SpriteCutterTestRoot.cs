using System;
using System.Collections;
using System.Collections.Generic;
using DataSystem;
using FurryCutter.Application.Repositories;
using FurryCutter.Bonuses;
using FurryCutter.Gameplay.ScoringSystem;
using FurryCutter.Gameplay.Session.ServiceLocator;
using FurryCutter.Gameplay.SpawningSystem;
using FurryCutter.Meta;
using FurryCutter.Presentation.UI;
using FurryCutter.Presentation.Visual;
using GenericUI;
using UnityEngine;
using UnitySpriteCutter.Control;
using UnitySpriteCutter.CutPostProcessing;
using YG;

internal class SpriteCutterTestRoot : MonoBehaviour
{
    [SerializeField] private LayerMask _cuttingLayer;
    [SerializeField] private MouseInputRepresentation _lineView;
    [SerializeField] private SessionScene _scene;
    [SerializeField] private float _forceAfterCut = 5f;
    [SerializeField] private float _sizeToClearCuttable = 0.15f;

    [Space]
    [SerializeField] private Transform _point;

    [Header("Data")]
    [SerializeField] private DataContext _dataContext;

    [Header("Timer")]
    [SerializeField] private int _sessionTime;
    [SerializeField] private AllFurriesConfig _furriesConfig;

    [Header("Spawn")]
    [SerializeField] private ValueRange _spawnRotationRange;
    [SerializeField] private float _minInterval;
    [SerializeField] private float _maxInterval;

    [Header("Bonuses")]
    [SerializeField] private BonusesHolder _bonusesHolder;

    [Header("Score bonus")]
    [SerializeField] private ComboRuleConfig _comboRuleConfig;

    [Header("UI")]
    [SerializeField] private BonusMenuPresenter _bonusMenuPresenter;
    [SerializeField] private ScoringSystemPresenter _sessionScoreView;
    [SerializeField] private TimerView _timerView;

    private StarLinesCutBonusPlacer _starLinesCutBonusPlacer;
    private LinesCutBonusPlacer _linesCutBonusPlacer;
    private TemporalFieldBonusPlacer _temporalFieldBonusPlacer;
    private IBonusSystem _bonusSystem;

    private List<IDisposable> _disposables = new List<IDisposable>();

    private IEnumerator Start()
    {
        while (YandexGame.SDKEnabled == false)
        {
            yield return null;
        }

        _dataContext.Load(new GameData());

        var serviceLocator = new SessionServiceLocator();
        serviceLocator.AddService(_scene);

        var lineInput = new LeanTouchLineInput();
        
        var cuttingSystem = new LinecastCuttingSystem(_cuttingLayer.value);
        var cutPostProcessingSystem = new CutPostProcessorsHolder(cuttingSystem);

        cutPostProcessingSystem.AddPostProcessor<AddingForcePostProcessor>(new AddingForcePostProcessor(5f));
        cutPostProcessingSystem.AddPostProcessor<SmallPiecesCleaner>(new SmallPiecesCleaner(_sizeToClearCuttable));
        cutPostProcessingSystem.AddPostProcessor<AddingComponentPostProcessor<Rigidbody2DSlowable>>(new AddingComponentPostProcessor<Rigidbody2DSlowable>());
        cutPostProcessingSystem.AddPostProcessor<ScoreHolderCopyingPostProcessor>(new ScoreHolderCopyingPostProcessor());

        new CuttingSystemInput(lineInput, cuttingSystem);

        serviceLocator.AddService<ICuttingSystem>(cuttingSystem);
        serviceLocator.AddService<ICutPostProcessorsHolder>(cutPostProcessingSystem);

        var score = new SessionScore();
        var comboSystem = new CutComboSystem(cutPostProcessingSystem, score, _comboRuleConfig, this);
        serviceLocator.AddService<IComboSystem>(comboSystem);
        serviceLocator.AddService<IScore>(score);

        var bonusRepository = new BonusesRepository(_dataContext, _bonusesHolder);
        var bonuses = bonusRepository.CreateAllPlacer(serviceLocator);
        _bonusSystem = new BonusSystem(bonuses);

        serviceLocator.AddService<IBonusSystem>(_bonusSystem);

        _lineView.Init(lineInput);

        var timer = new Timer(this);
        var timeTrigger = new RandomIntervalTrigger(_minInterval, _maxInterval, timer);

        //-------------

        var furriesRepository = new FurriesRepository(_dataContext, _furriesConfig);
        var furries = furriesRepository.GetOpenedRandomables();

        //--------------

        var spawningSystem = new FurryIntervalSpawner(serviceLocator, _spawnRotationRange, timeTrigger, furries);

        _timerView.SetTimer(timer);

        timer.StartTimer(_sessionTime);

        _disposables.Add(cutPostProcessingSystem);
        _disposables.Add(timeTrigger);
        _disposables.Add(spawningSystem);
        _disposables.Add(comboSystem);
        _disposables.Add(lineInput);

        _sessionScoreView.Init(comboSystem, score);
        _bonusMenuPresenter.Init(bonusRepository.GetAll(), serviceLocator.GetService<IBonusSystem>(), serviceLocator.GetService<SessionScene>().SceneSessionParent);
    }

    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }

    [EditorButton]
    private void SpawnStarCut()
    {
        _bonusSystem.PlaceBonus(BonusType.StarCut, _point.position);
    }

    [EditorButton]
    private void SpawnLinesCut()
    {
        _bonusSystem.PlaceBonus(BonusType.LinesCut, _point.position);
    }

    [EditorButton]
    private void SpawnTemporalField()
    {
        _bonusSystem.PlaceBonus(BonusType.TemporalField, _point.position);
    }
}