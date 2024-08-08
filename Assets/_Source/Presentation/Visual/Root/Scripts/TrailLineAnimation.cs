using System;
using DG.Tweening;
using UnityEngine;

namespace FurryCutter.Presentation.Visual
{
    [RequireComponent(typeof(TrailRenderer))]
    public class TrailLineAnimation : MonoBehaviour
    {
        [SerializeField] private float _moveTime = 0.5f;
        [SerializeField] private float _hideOffset = 0.5f;
        [SerializeField] private Ease _moveEase = Ease.InCubic;

        private Tween _movingTween;

        public void Move(Vector3 from, Vector3 to)
        {
            if (_movingTween != null)
                throw new ArgumentException("TrailLine is already moving");

            transform.position = from;
            _movingTween = transform.DOMove(to, _moveTime).SetEase(_moveEase);
            _movingTween.onComplete += () => Destroy(gameObject, _hideOffset);
        }
    }
}