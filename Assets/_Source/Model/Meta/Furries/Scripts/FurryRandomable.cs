using System;
using FurryCutter.Gameplay.ScoringSystem;
using RandomSpawnSystem;
using UnityEngine;
using UnitySpriteCutter;

namespace FurryCutter.Meta
{
    public class FurryRandomable : IWeightedRandomable
    {
        private Func<Transform, ScoreHolder> _createGameplayObject;
        private float _spawnWeight;

        public FurryRandomable(Func<Transform, ScoreHolder> createGameplayObject, float spawnWeight)
        {
            if (spawnWeight < 0 || spawnWeight > 1)
                throw new ArgumentException($"{nameof(spawnWeight)} should be between 0 and 1");

            _createGameplayObject = createGameplayObject;
            _spawnWeight = spawnWeight;
        }
        
        public Func<Transform, ScoreHolder> CreateGameplayObject => _createGameplayObject;
        public float GetNormalizedSpawnWeight() => _spawnWeight;
    }
}