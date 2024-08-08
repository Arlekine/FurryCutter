using UnityEngine;
using UnitySpriteCutter.Control;

namespace UnitySpriteCutter.CutPostProcessing
{
    public class AddingForcePostProcessor : ICutPostProcessor
    {
        private float _cuttedForce = 5f;

        public AddingForcePostProcessor(float cuttedForce)
        {
            _cuttedForce = cuttedForce;
        }

        public void PostProcessCut(LineCutResult[] cutResults)
        {
            foreach (var result in cutResults)
            {
                foreach (var cuttable in result.ResultCuttables)
                {
                    var cuttableCenter = (Vector2)cuttable.RealCenter;
                    cuttable.GetOrAddRigidbody().AddForce((cuttableCenter - result.Line.FindNearestPointOnLine(cuttableCenter)).normalized * _cuttedForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}