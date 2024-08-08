using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySpriteCutter.CutPostProcessing;

namespace FurryCutter.Gameplay.ScoringSystem
{
    public class CutComboSystem : IComboSystem, IDisposable
    {
        private ScoreHolderCopyingPostProcessor _scoreHoldersPostProcessor;
        private ComboRuleConfig _comboRuleConfig;
        private IScore _score;
        private MonoBehaviour _context;

        private Coroutine _comboRoutine;
        private int _currentFrameCombo;
        private int _currentFrameScore;

        public event Action<int, int> ComboMade;

        public CutComboSystem(ICutPostProcessorsHolder cutsCutPostProcessors, IScore score, ComboRuleConfig config, MonoBehaviour context)
        {
            _scoreHoldersPostProcessor = cutsCutPostProcessors.GetPostProcessor<ScoreHolderCopyingPostProcessor>();
            _comboRuleConfig = config;
            _score = score;
            _context = context;

            _comboRoutine = _context.StartCoroutine(ComboCalculatingRoutine());
            _scoreHoldersPostProcessor.NewScoreHoldersAppeared += OnCut;
        }

        public void Dispose()
        {
            _context.StopCoroutine(_comboRoutine);
            _scoreHoldersPostProcessor.NewScoreHoldersAppeared -= OnCut;
        }

        private void OnCut(ScoreHolder[] scoreHolders)
        {
            var scoreSumPerCut = 0;

            for (int i = 0; i < scoreHolders.Length; i++)
            {
                scoreSumPerCut += scoreHolders[i].Score;
            }

            scoreSumPerCut = (int)(scoreSumPerCut * _comboRuleConfig.GetMultiplayerForPiecesAmount(scoreHolders.Length));
            _currentFrameScore += scoreSumPerCut;
            _currentFrameCombo += scoreHolders.Length;
        }

        private IEnumerator ComboCalculatingRoutine()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();

                if (_currentFrameCombo > 0)
                {
                    ComboMade?.Invoke(_currentFrameScore, _currentFrameCombo);
                    _score.Add(_currentFrameScore);
                }

                _currentFrameCombo = 0;
                _currentFrameScore = 0;
            }
        }
    }
}