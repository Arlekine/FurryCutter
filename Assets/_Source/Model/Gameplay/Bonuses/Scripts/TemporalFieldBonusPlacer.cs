using UnityEngine;
using UnitySpriteCutter.Control;

namespace FurryCutter.Bonuses
{
    public class TemporalFieldBonusPlacer : IBonusPlacer
    {
        private TemporalField _temporalFieldPrefab;
        private float _destroyTime;

        private Transform _gameParent;

        public BonusType Type => BonusType.TemporalField;

        public float DestroyTime => _destroyTime;

        public TemporalFieldBonusPlacer(TemporalField temporalFieldPrefab, Transform gameParent, float destroyTime)
        {
            _temporalFieldPrefab = temporalFieldPrefab;
            _gameParent = gameParent;
            _destroyTime = destroyTime;
        }

        public void Place(Vector3 point)
        {
            var temporal = Object.Instantiate(_temporalFieldPrefab, _gameParent);
            temporal.transform.position = point;

            temporal.Show(_destroyTime);
        }
    }
}