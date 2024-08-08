using Unity.VisualScripting;
using UnityEngine;
using UnitySpriteCutter.Control;

namespace UnitySpriteCutter.CutPostProcessing
{
    public class AddingComponentPostProcessor<T> : ICutPostProcessor where T : Component
    {
        public void PostProcessCut(LineCutResult[] cutResults)
        {
            foreach (var result in cutResults)
            {
                foreach (var cuttable in result.ResultCuttables)
                {
                    cuttable.GetOrAddComponent<T>();
                }
            }
        }
    }
}