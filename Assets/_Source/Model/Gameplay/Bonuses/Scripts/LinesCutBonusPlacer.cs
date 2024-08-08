using UnityEngine;
using UnitySpriteCutter.Control;

namespace FurryCutter.Bonuses
{
    public class LinesCutBonusPlacer : IBonusPlacer
    {
        private Vector2 _�utDirection;
        private int _cutLinesAmount;
        private Vector2 _cuttingZoneSize;

        private ICuttingSystem _cuttingSystem;

        public BonusType Type => BonusType.LinesCut;

        public Vector2 �utDirection => _�utDirection;
        public int CutLinesAmount => _cutLinesAmount;
        public Vector2 CuttingZoneSize => _cuttingZoneSize;

        public LinesCutBonusPlacer(Vector2 �utDirection, int cutLinesAmount, Vector2 cuttingZoneSize, ICuttingSystem cuttingSystem)
        {
            _�utDirection = �utDirection;
            _cutLinesAmount = cutLinesAmount;
            _cuttingZoneSize = cuttingZoneSize;
            _cuttingSystem = cuttingSystem;
        }

        public void Place(Vector3 point)
        {
            var lines = MultipleLinesCreator.GetParallelLinesZone(point, _�utDirection, _cuttingZoneSize, _cutLinesAmount);
            _cuttingSystem.Cut(lines);
        }
    }
}