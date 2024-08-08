using System;
using System.Collections.Generic;
using FurryCutter.Gameplay.ScoringSystem;
using Unity.VisualScripting;
using UnitySpriteCutter.Control;

namespace UnitySpriteCutter.CutPostProcessing
{
    public class ScoreHolderCopyingPostProcessor : ICutPostProcessor
    {
        public Action<ScoreHolder[]> NewScoreHoldersAppeared;

        public void PostProcessCut(LineCutResult[] cutResults)
        {
            var newScoreHolders = new List<ScoreHolder>();

            foreach (var result in cutResults)
            {
                var oldScoreHolder = result.InitialCuttable.GetComponent<ScoreHolder>();

                if (oldScoreHolder != null)
                {
                    foreach (var cuttable in result.ResultCuttables)
                    {
                        var scoreHolder = cuttable.GetOrAddComponent<ScoreHolder>();
                        scoreHolder.Score = oldScoreHolder.Score;
                        newScoreHolders.Add(scoreHolder);
                    }
                }
            }

            NewScoreHoldersAppeared?.Invoke(newScoreHolders.ToArray());
        }
    }
}