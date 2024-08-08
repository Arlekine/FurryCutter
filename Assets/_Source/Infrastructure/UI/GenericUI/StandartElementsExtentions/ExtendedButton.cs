using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GenericUI
{
    public class ExtendedButton : Selectable
    {
        public UnityEvent OnClick;

        public UnityEvent OnHoldStart;
        public UnityEvent OnRelease;

        [Space] [SerializeField] private float _holdTime = 0.2f;

        private float _holdStartTime;
        private bool _isPressed;

        public bool IsHold { get; private set; }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            _isPressed = true;
            _holdStartTime = Time.time;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            _isPressed = false;

            if (IsHold == false)
            {
                OnClick?.Invoke();
            }
            else
            {
                IsHold = false;
                OnRelease?.Invoke();
            }
        }

        private void Update()
        {
            if (_isPressed)
            {
                if (IsHold == false && Time.time >= _holdStartTime + _holdTime)
                {
                    IsHold = true;
                    OnHoldStart?.Invoke();
                }
            }
        }
    }
}