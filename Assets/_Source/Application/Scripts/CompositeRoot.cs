using System;
using System.Collections;
using System.Collections.Generic;
using DataSystem;
using FurryCutter.Application;
using FurryCutter.Application.Repositories;
using FurryCutter.Bonuses;
using FurryCutter.Gameplay.Session;
using FurryCutter.Gameplay.Session.ServiceLocator;
using FurryCutter.Meta;
using FurryCutter.Presentation.UI;
using FurryCutter.Presentation.Visual;
using MetaGameplay;
using UnityEngine;
using YG;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private UI _ui;

    [Header("Session")]
    [SerializeField] private SessionVisualRoot _sessionVisualRoot;
    [SerializeField] private SessionConfig _sessionConfig;
    [SerializeField] private SessionScene _sessionScene;

    [Header("Data")]
    [SerializeField] private DataContext _dataContext;
    [SerializeField] private LevelRewardRule _rewardRule;
    [SerializeField] private string _leaderBoardName = "Main";

    [Header("Visual")]
    [SerializeField] private BackgroundVisual _backgroundVisual;

    [Header("Meta")]
    [SerializeField] private AllFurriesConfig _furriesConfig;
    [SerializeField] private BonusesHolder _bonusesHolder;

    private List<IDisposable> _disposables = new List<IDisposable>();
    private SessionCreator _session;
    private BonusesRepository _bonusesRepository;

    private IEnumerator Start()
    {
        while (YandexGame.SDKEnabled == false)
        {
            yield return null;
        }

        _dataContext.Load(new GameData());

        var coinsRepository = new CoinsRepository(_dataContext);

        var furriesRepository = new FurriesRepository(_dataContext, _furriesConfig);
        _bonusesRepository = new BonusesRepository(_dataContext, _bonusesHolder);

        _session = new SessionCreator(_bonusesRepository, furriesRepository, _sessionConfig, this._dataContext, _sessionScene);

        var leaderBoard = new LeaderBoard(_leaderBoardName);

        var scoreRepository = new ScoreRepository(_dataContext, leaderBoard, _session);
        var rewardGiver = new SessionRewardGiver(_rewardRule, coinsRepository.Get());
        
        _backgroundVisual.Init();

        _ui.Init(_session, _bonusesRepository, furriesRepository, rewardGiver, scoreRepository, leaderBoard, _backgroundVisual);
        _sessionVisualRoot.Init(_session);

        _disposables.Add(coinsRepository);
        _disposables.Add(furriesRepository);
        _disposables.Add(scoreRepository);
        _disposables.Add(leaderBoard);
        _disposables.Add(_bonusesRepository);
        _disposables.Add(_session);
    }

    [EditorButton]
    private void StartSession()
    {
        _session.Start();
    }

    [EditorButton]
    private void BuyBonuses()
    {
        _bonusesRepository.GetByType(BonusType.LinesCut).Add(5);
        _bonusesRepository.GetByType(BonusType.StarCut).Add(5);
        _bonusesRepository.GetByType(BonusType.TemporalField).Add(5);
    }

    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}

public enum RewardedAdType
{
    DoubleCoins = 0,
    Discount = 1
}