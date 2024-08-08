using FurryCutter.Gameplay.Session.ServiceLocator;
using FurryCutter.Presentation.Visual;
using UnityEngine;
using UnityEngine.UI;

namespace FurryCutter.Presentation.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        private IGameSessionCreator _sessionCreator;
        private BackgroundVisual _backgroundVisual;

        public void Init(IGameSessionCreator sessionCreator, BackgroundVisual backgroundVisual)
        {
            _sessionCreator = sessionCreator;
            _backgroundVisual = backgroundVisual;

            _backgroundVisual.ShowBackground(BackgroundVisual.BackType.MainMenu);

            _playButton.onClick.AddListener(StartSession);
        }

        public void Show()
        {
            _backgroundVisual.ShowBackground(BackgroundVisual.BackType.MainMenu);
            _playButton.gameObject.SetActive(true);
        }

        private void StartSession()
        {
            _sessionCreator.Start();
            
            _backgroundVisual.ShowBackground(BackgroundVisual.BackType.Session);
            _playButton.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(StartSession);
        }
    }
}