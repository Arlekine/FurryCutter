using UnitySpriteCutter.Control;

namespace UnitySpriteCutter.CutPostProcessing
{
    public interface ICutPostProcessor
    {
        void PostProcessCut(LineCutResult[] cutResults);
    }
}