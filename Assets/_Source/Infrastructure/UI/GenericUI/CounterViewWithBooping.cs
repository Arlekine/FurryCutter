using DG.Tweening;
using TMPro;
using UnityEngine;

namespace GenericUI
{
    public class CounterViewWithBooping : MonoBehaviour
    {
        [SerializeField] private TMP_Text _couter;

        [Space]
        [SerializeField] private float _boopScale = 1.1f;
        [SerializeField] private float _boopTime = 0.3f;

        private Sequence _currentBoopSequence;

        [EditorButton]
        public void SetText(string text)
        {
            _currentBoopSequence?.Kill();
            _currentBoopSequence = DOTween.Sequence();

            _currentBoopSequence.Append(transform.DOScale(_boopScale, _boopTime * 0.5f).SetEase(Ease.Linear));
            _currentBoopSequence.AppendCallback(() => _couter.text = text);
            _currentBoopSequence.Append(transform.DOScale(1f, _boopTime * 0.5f).SetEase(Ease.Linear));
            _currentBoopSequence.SetEase(Ease.InOutCubic);
        }

        [EditorButton]
        public void SetTextWithoutAnimation(string text)
        {
            _couter.text = text;
        }

        private void OnDestroy()
        {
            _currentBoopSequence?.Kill();
        }
    }
}