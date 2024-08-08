using UnityEngine;
using UnitySpriteCutter.Control;

namespace FurryCutter.Bonuses
{
    public class StarLinesCutBonusPlacer : IBonusPlacer
    {
        private int _cutLinesAmount;
        private float _linesLength;

        private ICuttingSystem _cuttingSystem;

        public BonusType Type => BonusType.StarCut;

        public int CutLinesAmount => _cutLinesAmount;
        public float LinesLength => _linesLength;

        public StarLinesCutBonusPlacer(int cutLinesAmount, float linesLength, ICuttingSystem cuttingSystem)
        {
            _cutLinesAmount = cutLinesAmount;
            _linesLength = linesLength;
            _cuttingSystem = cuttingSystem;
        }

        public void Place(Vector3 point)
        {
            var lines = MultipleLinesCreator.GetCircleLinesFromPoint(point, _linesLength, _cutLinesAmount);
            _cuttingSystem.Cut(lines);
        }
    }
}