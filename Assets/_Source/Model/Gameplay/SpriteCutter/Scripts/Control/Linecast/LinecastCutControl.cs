using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnitySpriteCutter.Control
{
    internal sealed class LinecastCutControl : ICutControl
    {
        private int _cutLayer = -1;

        public LinecastCutControl(int cutLayer)
        {
            _cutLayer = cutLayer;
        }

        public SpriteCutterOutput[] Cut(Line line)
        {
            List<Cuttable> gameObjectsToCut = new List<Cuttable>();
            RaycastHit2D[] hits = Physics2D.LinecastAll(line.LineStart, line.LineEnd, _cutLayer);

            foreach (RaycastHit2D hit in hits)
            {
                var cuttable = hit.collider.GetComponent<Cuttable>();
                if (cuttable)
                {
                    gameObjectsToCut.Add(cuttable);
                }
            }

            var output = new List<SpriteCutterOutput>();
            foreach (var cuttable in gameObjectsToCut)
            {
                output.Add(SpriteCutter.Cut(new SpriteCutterInput(line.LineStart, line.LineEnd, cuttable)));
            }

            return output.ToArray();
        }
    }
}