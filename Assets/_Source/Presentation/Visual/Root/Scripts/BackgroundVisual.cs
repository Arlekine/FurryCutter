using System.Collections.Generic;
using UnityEngine;

namespace FurryCutter.Presentation.Visual
{
    public class BackgroundVisual : MonoBehaviour
    {
        public enum BackType
        {
            Session,
            MainMenu
        }

        [SerializeField] private Background _sessionBackground;
        [SerializeField] private Background _mainMenu;

        private Dictionary<BackType, Background> _backgrounds = new Dictionary<BackType, Background>();

        public void Init()
        {
            _backgrounds.Add(BackType.Session, _sessionBackground);
            _backgrounds.Add(BackType.MainMenu, _mainMenu);

            foreach (var backType in _backgrounds.Keys)
            {
                _backgrounds[backType].HideInstantly();
            }
        }

        public void ShowBackground(BackType type)
        {
            foreach (var backType in _backgrounds.Keys)
            {
                if (backType == type)
                    _backgrounds[backType].Show();
                else
                    _backgrounds[backType].Hide();
            }
        }
    }
}