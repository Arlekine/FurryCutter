using DG.Tweening;
using UnityEngine;

namespace FurryCutter.Bonuses
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Rigidbody2DSlowable : MonoBehaviour, ISlowable
    {

        private Rigidbody2D _rigidbody;
        private float _normalizedSpeed = 1f;

        private Tween _speedChangingTween;

        public Rigidbody2D Rigidbody2D
        {
            get
            {
                if (_rigidbody == null)
                    _rigidbody = GetComponent<Rigidbody2D>();

                return _rigidbody;
            }
        }

        public float CurrentNormalizedSpeed
        {
            get => _normalizedSpeed;
            private set
            {
                Rigidbody2D.velocity *= value / _normalizedSpeed;
                Rigidbody2D.angularVelocity *= value / _normalizedSpeed;
                Rigidbody2D.gravityScale *= value / _normalizedSpeed;
                _normalizedSpeed = value;
            }
        }

        public void SetNormalizedSpeed(float speed)
        {
            _speedChangingTween?.Kill();
            _speedChangingTween = DOTween.To(() => CurrentNormalizedSpeed, sp => { CurrentNormalizedSpeed = sp; }, speed, 0.3f);
        }

        private void OnDestroy()
        {
            _speedChangingTween?.Kill();
        }
    }
}