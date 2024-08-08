using System;
using UnitySpriteCutter.Control;

namespace UnitySpriteCutter.CutPostProcessing
{
    public class FunctionPostProcessor : ICutPostProcessor
    {
        private Action<LineCutResult[]> _postProcessor;

        public FunctionPostProcessor(Action<LineCutResult[]> postProcessor)
        {
            _postProcessor = postProcessor;
        }

        public void PostProcessCut(LineCutResult[] cutResults)
        {
            _postProcessor(cutResults);
        }
    }
}