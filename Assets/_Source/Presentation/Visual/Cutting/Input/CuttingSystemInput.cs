using UnityEngine;

namespace UnitySpriteCutter.Control
{
    internal class CuttingSystemInput
    {
        private ILineInput _lineInput;
        private ICuttingSystem _cuttingSystem;

        public CuttingSystemInput(ILineInput lineInput, ICuttingSystem cuttingSystem)
        {
            _lineInput = lineInput;
            _cuttingSystem = cuttingSystem;

            _lineInput.Released += Cut;
        }

        private void Cut(Vector2 startScreenPos, Vector2 endScreenPos)
        {
            var line = new Line(Camera.main.ScreenToWorldPoint(startScreenPos), Camera.main.ScreenToWorldPoint(endScreenPos));
            _cuttingSystem.Cut(line);
        }
    }
}
