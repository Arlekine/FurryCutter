using DG.Tweening;
using UnityEngine;

namespace GenericTweenAnimation
{
    public abstract class TweenAnimation : MonoBehaviour
    {
        protected Tween _currentAnimation;

        public abstract  Tween Play();

        protected virtual void OnDestroy()
        {
            _currentAnimation?.Kill();
        }
    }
}
