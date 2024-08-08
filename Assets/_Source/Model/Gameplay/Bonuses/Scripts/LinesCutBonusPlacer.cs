using UnityEngine;
using UnitySpriteCutter.Control;

namespace FurryCutter.Bonuses
{
    public class LinesCutBonusPlacer : IBonusPlacer
    {
        private Vector2 _ñutDirection;
        private int _cutLinesAmount;
        private Vector2 _cuttingZoneSize;

        private ICuttingSystem _cuttingSystem;

        public BonusType Type => BonusType.LinesCut;

        public Vector2 ÑutDirection => _ñutDirection;
        public int CutLinesAmount => _cutLinesAmount;
        public Vector2 CuttingZoneSize => _cuttingZoneSize;

        public LinesCutBonusPlacer(Vector2 ñutDirection, int cutLinesAmount, Vector2 cuttingZoneSize, ICuttingSystem cuttingSystem)
        {
            _ñutDirection = ñutDirection;
            _cutLinesAmount = cutLinesAmount;
            _cuttingZoneSize = cuttingZoneSize;
            _cuttingSystem = cuttingSystem;
        }

        public void Place(Vector3 point)
        {
            var lines = MultipleLinesCreator.GetParallelLinesZone(point, _ñutDirection, _cuttingZoneSize, _cutLinesAmount);
            _cuttingSystem.Cut(lines);
        }
    }
}