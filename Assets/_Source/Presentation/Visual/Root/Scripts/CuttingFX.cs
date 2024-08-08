using UnityEngine;
using UnitySpriteCutter.Control;

namespace FurryCutter.Presentation.Visual
{
    public class CuttingFX : MonoBehaviour
    {
        [SerializeField] private TrailLineAnimation _cutPrefab;

        private ICuttingSystem _cuttingSystem;
        private Transform _game;

        public void Init(ICuttingSystem cuttingSystem, Transform game)
        {
            _cuttingSystem = cuttingSystem;
            _game = game;

            _cuttingSystem.Cutted += OnCut;
        }

        private void OnCut(LineCutResult[] cuts)
        {
            foreach (var cutResult in cuts)
            {
                var newFX = Instantiate(_cutPrefab, _game);
                newFX.Move(cutResult.Line.LineStart, cutResult.Line.LineEnd);
            }
        }
    }
}