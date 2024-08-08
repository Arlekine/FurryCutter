using System;
using GenericTweenAnimation;
using UnityEngine;

namespace FurryCutter.Presentation.UI.SessionFinal
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private RunningNumberView _scoreView;
        [SerializeField] private UIShowingAnimation _showingAnimation;
        [SerializeField] private TweenAnimation _finalAnimation;
        [SerializeField] private ParticleSystem _finalFX;
        [SerializeField] private float _fxOffset;
        [SerializeField] private float _animationEndOffset;

        public void Show(int score, Action showed)
        {
            _showingAnimation.Show();
            _scoreView.ShowNumber(0, score, "{0}", () =>
            {
                _finalAnimation.Play();
                this.ActionAfterPause(_fxOffset, () => Instantiate(_finalFX, transform));
                this.ActionAfterPause(_animationEndOffset, () => showed?.Invoke());
            });
        }

        [EditorButton]
        public void Play()
        {
            _finalAnimation.Play();
            this.ActionAfterPause(_fxOffset, () => Instantiate(_finalFX, transform));
        }

        public void Prewarm()
        {
            _showingAnimation.HideInstantly();
        }
    }
}