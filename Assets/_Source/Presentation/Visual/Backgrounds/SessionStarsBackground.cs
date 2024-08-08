using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace FurryCutter.Presentation.Visual
{
    public class SessionStarsBackground : Background
    {
        [Serializable]
        private class BackgroundStar
        {
            [SerializeField] private Transform _star;
            [SerializeField] private float _minPulseScale;
            [SerializeField] private float _maxPulseScale;
            [SerializeField] private float _initialShowTime = 0.4f;
            [SerializeField] private float _pulseTime;

            private Sequence _pulsingTween;
            private Sequence _showHideTween;

            public float MinPulseScale => _minPulseScale;
            public float MaxPulseScale => _maxPulseScale;

            public Tween Show()
            {
                _pulsingTween?.Kill();
                _showHideTween?.Kill();

                _showHideTween = DOTween.Sequence();
                _showHideTween.Append(_star.DOScale(_minPulseScale, _initialShowTime).SetEase(Ease.OutBack));
                _showHideTween.onComplete += () => StartPulsing();

                return _showHideTween;
            }

            public Tween Hide()
            {
                _pulsingTween?.Kill();
                _showHideTween?.Kill();

                _showHideTween = DOTween.Sequence();
                _showHideTween.Append(_star.DOScale(0f, _initialShowTime).SetEase(Ease.InBack));
                return _showHideTween;
            }

            public void HideInstantly()
            {
                _star.localScale = Vector3.zero;
            }

            public Tween StartPulsing()
            {
                _star.transform.localScale = Vector3.one * _minPulseScale;

                _pulsingTween?.Kill();
                _pulsingTween = DOTween.Sequence();
                _pulsingTween.Append(_star.DOScale(_maxPulseScale, _pulseTime * 0.5f).SetEase(Ease.Linear));
                _pulsingTween.Append(_star.DOScale(_minPulseScale, _pulseTime * 0.5f).SetEase(Ease.Linear));
                _pulsingTween.SetEase(Ease.InOutCubic);
                _pulsingTween.SetLoops(-1);

                return _pulsingTween;
            }
        }

        [SerializeField] private List<BackgroundStar> _stars = new List<BackgroundStar>();

        [EditorButton]
        public override void Show()
        {
            foreach (var star in _stars)
            {
                star.Show();
            }
        }

        [EditorButton]
        public override void Hide()
        {
            foreach (var star in _stars)
            {
                star.Hide();
            }
        }

        public override void HideInstantly()
        {
            foreach (var star in _stars)
            {
                star.HideInstantly();
            }
        }
    }
}
