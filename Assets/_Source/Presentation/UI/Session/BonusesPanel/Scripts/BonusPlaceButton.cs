using System;
using DG.Tweening;
using Lean.Touch;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GenericUI
{
    public class BonusPlaceButton : MonoBehaviour, ILeanSelectable
    {
        [SerializeField] private RectTransform _mainRect;
        [SerializeField] private Image _icon;
        [SerializeField] private LeanSelectable _selectable;

        [Header("Quantity part")]
        [SerializeField] private RectTransform _quantityRect;
        [SerializeField] private TMP_Text _quantityText;
        [SerializeField] private Graphic _quantityBack;

        [Header("Quantity color")]
        [SerializeField] private Color _defaultQuantityColor = Color.white;
        [SerializeField] private Color _zeroQuantityColor = Color.white;

        [Header("Quantity Boop")]
        [SerializeField] private float _boopSize = 1.1f;
        [SerializeField] private float _boopStepTime = 0.15f;

        [Header("States")]
        [SerializeField] private CanvasGroup _fadeGroup;
        [SerializeField] private Color _quantityBackActivatedColor = Color.white;
        [SerializeField] private Color _quantityBackDeactivatedColor = Color.white;
        [SerializeField] private float _activatedScale = 1f;
        [SerializeField] private float _deactivatedScale = 0.9f;
        [SerializeField] private float _stateSwitchTime = 0.3f;

        private Sequence _currentBoopSequence;

        public event Action<LeanFinger, ILeanSelectable> Selected;

        public void SetIcon(Sprite icon) => _icon.sprite = icon;

        public void SetQuantity(int count, bool isSilent)
        {
            _currentBoopSequence?.Kill();
            _currentBoopSequence = DOTween.Sequence();

            if (isSilent)
            {
                SetQuantityValue(count);
            }
            else
            {
                _currentBoopSequence.Append(_quantityRect.DOScale(_boopSize, _boopStepTime).SetEase(Ease.Linear));
                _currentBoopSequence.AppendCallback(() =>
                {
                    SetQuantityValue(count);
                });

                _currentBoopSequence.Append(_quantityRect.DOScale(1f, _boopStepTime).SetEase(Ease.Linear));
                _currentBoopSequence.SetEase(Ease.InOutQuad);
            }
        }

        public void Activate()
        {
            _mainRect.DOScale(_activatedScale, _stateSwitchTime);
            _fadeGroup.DOFade(0f, _stateSwitchTime);
            _quantityBack.DOColor(_quantityBackActivatedColor, _stateSwitchTime);
            _selectable.enabled = true;
        }

        public void Deactivate()
        {
            _mainRect.DOScale(_deactivatedScale, _stateSwitchTime);
            _fadeGroup.DOFade(1f, _stateSwitchTime);
            _quantityBack.DOColor(_quantityBackDeactivatedColor, _stateSwitchTime);
            _selectable.enabled = false;
        }

        private void OnEnable()
        {
            _selectable.OnSelect.AddListener(OnSelected);
        }

        private void OnDisable()
        {
            _selectable.OnSelect.RemoveListener(OnSelected);
        }

        private void OnDestroy()
        {
            _quantityText.DOKill();
            _fadeGroup.DOKill();
            _quantityBack.DOKill();
            _mainRect.DOKill();
            _currentBoopSequence?.Kill();
        }

        private void SetQuantityValue(int count)
        {
            _quantityText.text = count.ToString();
            _quantityText.color = count > 0 ? _defaultQuantityColor : _zeroQuantityColor;
        }

        private void OnSelected(LeanFinger finger)
        {
            Selected?.Invoke(finger, this);
        }
    }
}