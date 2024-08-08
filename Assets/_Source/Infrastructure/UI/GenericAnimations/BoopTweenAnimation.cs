using DG.Tweening;
using UnityEngine;

namespace GenericTweenAnimation
{
    public class BoopTweenAnimation : TweenAnimation
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _maxScale;
        [SerializeField] private float _time;

        public override Tween Play()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_target.DOScale(_maxScale, _time * 0.5f).SetEase(Ease.Linear));
            sequence.Append(_target.DOScale(1f, _time * 0.5f).SetEase(Ease.Linear));
            sequence.SetEase(Ease.OutQuart);

            _currentAnimation = sequence;
            return _currentAnimation;
        }
    }
}