using System;
using Lean.Touch;
using UnityEngine;

namespace UnitySpriteCutter.Control
{
    public class LeanTouchLineInput : ILineInput, IDisposable
    {
        private LeanFinger _currentFinger;
        private bool _isEnabled;

        private Vector2 _pressStart;

        public event Action<Vector2> Pressed;
        public event Action<Vector2, Vector2> Released;

        public LeanTouchLineInput()
        {
            _isEnabled = true;
            LeanTouch.OnFingerDown += OnFingerDown;
            LeanTouch.OnFingerUp += OnFingerUp;
        }

        public bool IsPressed => _currentFinger != null;
        public Vector2 CurrentPointerPosition
        {
            get
            {
                if (IsPressed == false)
                    throw new Exception("Can't get pointer position, because pointer isn't pressed");

                return _currentFinger.ScreenPosition;
            }
        }

        public void Enable() => _isEnabled = true;

        public void Disable() => _isEnabled = false;

        public void Dispose()
        {
            LeanTouch.OnFingerDown -= OnFingerDown;
            LeanTouch.OnFingerUp -= OnFingerUp;
        }

        private void OnFingerDown(LeanFinger finger)
        {
            if (_currentFinger == null && _isEnabled && finger.StartedOverGui == false)
            {
                _currentFinger = finger;
                _pressStart = finger.ScreenPosition;
                Pressed?.Invoke(finger.ScreenPosition);
            }
        }

        private void OnFingerUp(LeanFinger finger)
        {
            if (_currentFinger == finger)
            {
                Released?.Invoke(_pressStart, _currentFinger.ScreenPosition);
                _currentFinger = null;
            }
        }
    }
}