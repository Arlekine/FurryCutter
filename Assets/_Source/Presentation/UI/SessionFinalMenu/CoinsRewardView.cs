using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FurryCutter.Presentation.UI.SessionFinal
{
    public class CoinsRewardView : MonoBehaviour
    {
        private const string CoinsFormat = "+{0}";

        [SerializeField] private TMP_Text _text;
        [SerializeField] private UIShowingAnimation _selectionAnimation;
        [SerializeField] private ParticleSystem _selectedFX;

        [EditorButton]
        public void Set(int coins)
        {
            _text.text = String.Format(CoinsFormat, coins.ToString());
        }

        public void Prewarm()
        {
            _selectionAnimation.HideInstantly();
            _selectedFX.Stop();
        }

        [EditorButton]
        public void Select(int coins)
        {
            _text.text = String.Format(CoinsFormat, coins.ToString());
            _selectedFX.Play();
            _selectionAnimation.Show();
        }
    }
}