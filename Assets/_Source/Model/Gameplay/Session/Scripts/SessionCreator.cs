using System;
using System.Collections.Generic;
using FurryCutter.Application.Repositories;
using FurryCutter.Bonuses;
using FurryCutter.Gameplay.ScoringSystem;
using FurryCutter.Gameplay.Session;
using FurryCutter.Gameplay.Session.ServiceLocator;
using FurryCutter.Gameplay.SpawningSystem;
using UnityEngine;
using UnitySpriteCutter.Control;
using UnitySpriteCutter.CutPostProcessing;

namespace FurryCutter.Application
{
    public class SessionCreator : IGameSessionCreator, IDisposable
    {
        private BonusesRepository _bonusesRepository;
        private FurriesRepository _furriesRepository;

        private SessionConfig _sessionConfig;
        private MonoBehaviour _context;

        private SessionScene _scene;

        private List<IDisposable> _disposables = new List<IDisposable>();

        private Timer _timer;
        private SessionServiceLocator _serviceLocator;

        public event Action<IServiceLocator<ISessionService>> Started;
        public event Action<int> Completed;

        public IServiceLocator<ISessionService> CurrentServiceLocator => _serviceLocator;

        public SessionCreator(BonusesRepository bonusesRepository, FurriesRepository furriesRepository, SessionConfig sessionConfig, 
            MonoBehaviour context, SessionScene scene)
        {
            _bonusesRepository = bonusesRepository;
            _furriesRepository = furriesRepository;
            _sessionConfig = sessionConfig;
            _context = context;
            _scene = scene;
        }
        
        public void Start()
        {
            _serviceLocator = new SessionServiceLocator();
            _serviceLocator.AddService(_scene);

            var input = new LeanTouchLineInput();
            _serviceLocator.AddService<ILineInput>(input);

            var lineInput = new LeanTouchLineInput();

            var cuttingSystem = new LinecastCuttingSystem(_sessionConfig.CuttingLayer.value);
            var cutPostProcessingSystem = new CutPostProcessorsHolder(cuttingSystem);

            cutPostProcessingSystem.AddPostProcessor<AddingForcePostProcessor>(new AddingForcePostProcessor(5f));
            cutPostProcessingSystem.AddPostProcessor<SmallPiecesCleaner>(new SmallPiecesCleaner(_sessionConfig.SizeToClearCuttable));
            cutPostProcessingSystem.AddPostProcessor<AddingComponentPostProcessor<Rigidbody2DSlowable>>(new AddingComponentPostProcessor<Rigidbody2DSlowable>());
            cutPostProcessingSystem.AddPostProcessor<ScoreHolderCopyingPostProcessor>(new ScoreHolderCopyingPostProcessor());

            new CuttingSystemInput(lineInput, cuttingSystem);

            _serviceLocator.AddService<ICuttingSystem>(cuttingSystem);
            _serviceLocator.AddService<ICutPostProcessorsHolder>(cutPostProcessingSystem);

            var score = new SessionScore();
            var comboSystem = new CutComboSystem(cutPostProcessingSystem, score, _sessionConfig.ComboRuleConfig, _context);
            _serviceLocator.AddService<IComboSystem>(comboSystem);
            _serviceLocator.AddService<IScore>(score);

            var bonuses = _bonusesRepository.CreateAllPlacer(_serviceLocator);
            var bonusSystem = new BonusSystem(bonuses);

            _serviceLocator.AddService<IBonusSystem>(bonusSystem);

            _timer = new Timer(_context);
            var timerHolder = new TimerHolder(_timer);
            var timeTrigger = new RandomIntervalTrigger(_sessionConfig.MinSpawnInterval, _sessionConfig.MaxSpawnInterval, _timer);

            _serviceLocator.AddService(timerHolder);

            var furries = _furriesRepository.GetOpenedRandomables();

            var spawningSystem = new FurryIntervalSpawner(_serviceLocator, _sessionConfig.SpawnRotationRange, timeTrigger, furries);

            _timer.Expired += OnTimerExpired;
            _timer.StartTimer(_sessionConfig.SessionTime);

            _disposables.Add(cutPostProcessingSystem);
            _disposables.Add(timeTrigger);
            _disposables.Add(spawningSystem);
            _disposables.Add(comboSystem);
            _disposables.Add(lineInput);

            Started?.Invoke(_serviceLocator);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }

        public void Pause() => _timer.Pause();

        public void Continue() => _timer.Continue();

        private void OnTimerExpired()
        {
            Completed?.Invoke(_serviceLocator.GetService<IScore>().CurrentScore);
            ClearCurrentSession();
        }

        private void ClearCurrentSession()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            _timer = null;
            _serviceLocator = null;
        }
    }
}