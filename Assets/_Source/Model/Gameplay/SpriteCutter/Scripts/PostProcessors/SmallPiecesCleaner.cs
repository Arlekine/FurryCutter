using System;
using UnityEngine;
using UnitySpriteCutter.Control;
using Object = UnityEngine.Object;

namespace UnitySpriteCutter.CutPostProcessing
{
    public class SmallPiecesCleaner : ICutPostProcessor
    {
        private float _squareToDestroy = 0.35f;

        public event Action<Vector3> CuttableCleaned;

        public SmallPiecesCleaner(float squareToDestroy)
        {
            _squareToDestroy = squareToDestroy;
        }

        public void PostProcessCut(LineCutResult[] results)
        {
            foreach (var cutResult in results)
            {
                foreach (var cuttable in cutResult.ResultCuttables)
                {
                    if (cuttable.BoundsSquare <= _squareToDestroy)
                    {
                        Object.Destroy(cuttable.gameObject);
                        CuttableCleaned?.Invoke(cuttable.RealCenter);
                    }
                }
            }
        }
    }
}