using System;
using System.Collections.Generic;

namespace UnitySpriteCutter.Control
{
    public class LinecastCuttingSystem : ICuttingSystem
    {
        private class CutResultHelper
        {
            public Line Line;
            public Cuttable OldCuttable;
            public List<Cuttable> ResultCuttables;

            public CutResultHelper(Line line, Cuttable oldCuttable, List<Cuttable> resultCuttables)
            {
                Line = line;
                OldCuttable = oldCuttable;
                ResultCuttables = resultCuttables;
            }

            public LineCutResult GetLineCutResult() => new LineCutResult(Line, OldCuttable, ResultCuttables.ToArray());
        }

        private ICutControl _cutControl;

        public LinecastCuttingSystem(int cutLayer)
        {
            _cutControl = new LinecastCutControl(cutLayer);
        }

        public event Action<LineCutResult[]> Cutted;

        public void Cut(Line cutLine)
        {
            var output = _cutControl.Cut(cutLine);

            if (output.Length == 0)
                return;

            var cutResults = new List<LineCutResult>();

            foreach (var spriteCutterOutput in output)
            {
                cutResults.Add(new LineCutResult(cutLine, spriteCutterOutput.oldCuttable, spriteCutterOutput.NewCuttables.ToArray()));
            }

            Cutted?.Invoke(cutResults.ToArray());
        }

        public void Cut(IEnumerable<Line> cutLine)
        {
            var intermediateResults = new List<CutResultHelper>();
            foreach (var line in cutLine)
            {
                var output = _cutControl.Cut(line);

                if (output.Length == 0)
                    continue;

                foreach (var spriteCutterOutput in output)
                {
                    var old = spriteCutterOutput.oldCuttable;

                    var prevResult = intermediateResults.Find(helper => helper.ResultCuttables.Contains(old));
                    if (prevResult != null)
                    {
                        prevResult.ResultCuttables.Remove(old);
                        if (prevResult.ResultCuttables.Count == 0)
                            intermediateResults.Remove(prevResult);

                        old = prevResult.OldCuttable;
                    }

                    intermediateResults.Add(new CutResultHelper(line, old, spriteCutterOutput.NewCuttables));
                }
            }

            var result = new List<LineCutResult>();

            foreach (var intermediateResult in intermediateResults)
            {
                result.Add(intermediateResult.GetLineCutResult());
            }

            Cutted?.Invoke(result.ToArray());
        }
    }
}