using System;
using System.Collections.Generic;
using UnitySpriteCutter.Control;

namespace UnitySpriteCutter.CutPostProcessing
{
    public class CutPostProcessorsHolder : ICutPostProcessorsHolder, IDisposable
    {
        private ICuttingSystem _cuttingSystem;

        private Dictionary<Type, ICutPostProcessor> _postProcessors = new Dictionary<Type, ICutPostProcessor>();

        public CutPostProcessorsHolder(ICuttingSystem cuttingSystem)
        {
            _cuttingSystem = cuttingSystem;
            _cuttingSystem.Cutted += PostProcessCut;
        }

        public void AddPostProcessor<T>(ICutPostProcessor postProcessor) where T : ICutPostProcessor => _postProcessors.Add(typeof(T), postProcessor);

        public T GetPostProcessor<T>() where T : ICutPostProcessor
        {
            if (HasPostProcessor<T>() == false)
                throw new ArgumentException($"System doesn't contains postprocessor {typeof(T)}");

            return (T)_postProcessors[typeof(T)];
        }

        public bool HasPostProcessor<T>() where T : ICutPostProcessor => _postProcessors.ContainsKey(typeof(T));

        private void PostProcessCut(LineCutResult[] cutResults)
        {
            foreach (var cutPostProcessor in _postProcessors.Keys)
            {
                _postProcessors[cutPostProcessor].PostProcessCut(cutResults);
            }
        }

        public void Dispose()
        {
            _cuttingSystem.Cutted -= PostProcessCut;
        }
    }
}