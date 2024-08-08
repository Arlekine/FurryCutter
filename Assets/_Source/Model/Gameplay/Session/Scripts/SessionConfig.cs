using FurryCutter.Gameplay.ScoringSystem;
using UnityEngine;

namespace FurryCutter.Gameplay.Session.ServiceLocator
{
    [CreateAssetMenu(menuName = "Data/Gameplay/SessionCreator Config", fileName = "SessionConfig")]
    public class SessionConfig : ScriptableObject
    {
        [SerializeField] private LayerMask _cuttingLayer;
        [SerializeField] private float _forceAfterCut = 5f;
        [SerializeField] private float _sizeToClearCuttable = 0.15f;
        [SerializeField] private ComboRuleConfig _comboRuleConfig;

        [Header("Spawning")]
        [SerializeField] private float _minStartRotation;
        [SerializeField] private float _maxStartRotation;
        [SerializeField] private float _minSpawnInterval;
        [SerializeField] private float _maxSpawnInterval;

        [Header("Time")] [SerializeField] private int _sessionTime;

        public LayerMask CuttingLayer => _cuttingLayer;
        public float ForceAfterCut => _forceAfterCut;
        public float SizeToClearCuttable => _sizeToClearCuttable;
        public ComboRuleConfig ComboRuleConfig => _comboRuleConfig;

        public ValueRange SpawnRotationRange => new ValueRange(_minStartRotation, _maxStartRotation);
        public float MinSpawnInterval => _minSpawnInterval;
        public float MaxSpawnInterval => _maxSpawnInterval;

        public int SessionTime => _sessionTime;

        private void OnValidate()
        {
            if (_minSpawnInterval >= _maxSpawnInterval)
                _maxSpawnInterval = _minSpawnInterval + 0.1f;
            
            if (_minStartRotation >= _maxStartRotation)
                _maxStartRotation = _minStartRotation + 0.1f;
        }
    }
}