using FurryCutter.Gameplay.Session.ServiceLocator;
using UnityEngine;
using UnitySpriteCutter.Control;

namespace FurryCutter.Presentation.Visual
{
    public class SessionVisualRoot : MonoBehaviour
    {
        [SerializeField] private MouseInputRepresentation _cutLineVisualPrefab;
        [SerializeField] private CuttingFX _cuttingFXPrefab;

        private MouseInputRepresentation _cutLineInputRepresentation;
        private CuttingFX _cuttingFX;

        private IGameSessionCreator _gameSession;

        public void Init(IGameSessionCreator gameSession)
        {
            _gameSession = gameSession;

            _gameSession.Started += OnSessionStarted;
            _gameSession.Completed += OnSessionCompleted;
        }

        private void OnSessionStarted(IServiceLocator<ISessionService> sessionServiceLocator)
        {
            _cutLineInputRepresentation = Instantiate(_cutLineVisualPrefab, transform);
            _cutLineInputRepresentation.Init(sessionServiceLocator.GetService<ILineInput>());

            _cuttingFX = Instantiate(_cuttingFXPrefab, transform);
            _cuttingFX.Init(sessionServiceLocator.GetService<ICuttingSystem>(), sessionServiceLocator.GetService<SessionScene>().SceneSessionParent);
        }

        private void OnSessionCompleted(int finalScore)
        {
            Destroy(_cutLineInputRepresentation.gameObject);
            Destroy(_cuttingFX.gameObject);
        }

        private void OnDestroy()
        {
            if (_gameSession != null)
            {
                _gameSession.Started -= OnSessionStarted;
                _gameSession.Completed -= OnSessionCompleted;
            }
        }
    }
}
