using System;
using System.Collections;
using FurryCutter.Gameplay.ScoringSystem;
using GenericUI;
using TMPro;
using UnityEngine;

namespace FurryCutter.Presentation.UI
{
    public class ScoringSystemPresenter : MonoBehaviour
    {
        private const string ComboFormat = "{0} {1}x (+{2}!)";

        [SerializeField] private CounterViewWithBooping _scoreView;
        [SerializeField] private ComboViewConfig _comboViewConfig;

        [Space]
        [SerializeField] private TMP_Text _comboText;
        [SerializeField] private UIShowingAnimation _comboTextAnimation;
        [SerializeField] private float _hideOffset;

        private IComboSystem _currentSessionScore;
        private IScore _score;

        private Coroutine _hideRoutine;

        public void Init(IComboSystem comboSystem, IScore score)
        {
            _currentSessionScore = comboSystem;
            _score = score;

            _scoreView.SetText("");
            _comboText.text = "";

            _currentSessionScore.ComboMade += OnCombo;
            _score.Updated += OnScoreUpdated;
            _scoreView.SetTextWithoutAnimation("0");
        }

        private void OnCombo(int comboScore, int comboCount)
        {
            if (_hideRoutine != null)
                StopCoroutine(_hideRoutine);

            _comboTextAnimation.Show();
            _comboText.text = (String.Format(ComboFormat, _comboViewConfig.GetComboWordForComboCount(comboCount), comboCount, comboScore));

            _hideRoutine = StartCoroutine(HideComboTextRoutine());
        }

        private void OnScoreUpdated(int newScore)
        {
            _scoreView.SetText(newScore.ToString());
        }

        private void OnDestroy()
        {
            if (_currentSessionScore != null && _score != null)
            {
                _currentSessionScore.ComboMade -= OnCombo;
                _score.Updated -= OnScoreUpdated;
            }
        }

        private IEnumerator HideComboTextRoutine()
        {
            yield return new WaitForSeconds(_hideOffset);
            _comboTextAnimation.Hide();
        }
    }
}
