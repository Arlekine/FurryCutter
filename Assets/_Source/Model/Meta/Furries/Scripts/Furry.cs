using System;
using FurryCutter.Gameplay.ScoringSystem;
using UnityEngine;
using UnitySpriteCutter;
using Object = UnityEngine.Object;

namespace FurryCutter.Meta
{
    public class Furry
    {
        private FurryID _id;
        private Sprite _mainSprite;
        private float _spawnWeight;
        private int _cutScore;
        private ScoreHolder _gameplayPrefab;
        private bool _isOpened;

        public event Action<Furry, bool> DataUpdated;

        public Furry(FurryID id, Sprite mainSprite, ScoreHolder gameplayPrefab, float spawnWeight, int cutScore, bool isOpened)
        {
            _id = id;
            _mainSprite = mainSprite;
            _gameplayPrefab = gameplayPrefab;
            this._isOpened = isOpened;

            _spawnWeight = spawnWeight;
            _cutScore = cutScore;
        }

        public FurryID Id => _id;
        public Sprite MainSprite => _mainSprite;
        public float SpawnWeight => _spawnWeight;

        public Func<Transform, ScoreHolder> CreateGameplayObject => new Func<Transform, ScoreHolder>((gameParent) =>
        {
            var gameplayObject = Object.Instantiate(_gameplayPrefab, gameParent);
            gameplayObject.Score = _cutScore;
            return gameplayObject;
        });

        public bool IsOpened
        {
            get => _isOpened;
            set
            {
                if (value != _isOpened)
                {
                    _isOpened = value;
                    DataUpdated?.Invoke(this, _isOpened);
                }
            }
        }
    }
}