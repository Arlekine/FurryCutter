using FurryCutter.Application.Repositories;
using FurryCutter.Gameplay.Session.ServiceLocator;
using FurryCutter.Meta;
using FurryCutter.Presentation.UI.SessionFinal;
using FurryCutter.Presentation.Visual;
using UnityEngine;

namespace FurryCutter.Presentation.UI
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private Transform _canvasesParent;

        [Space]
        [SerializeField] private SessionUI _sessionUIPrefab;
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private SessionFinalMenu _finalMenu;

        private IGameSessionCreator _gameSessionCreator;
        private BonusesRepository _bonusesRepository;
        private FurriesRepository _furriesRepository;
        private ScoreRepository _scoreRepository;
        private SessionRewardGiver _sessionRewardGiver;
        private LeaderBoard _leaderBoard;
        private BackgroundVisual _backgrounds;

        private SessionUI _currentSessionUI;
        private SessionFinalMenu _currentFinalMenu;

        public void Init(IGameSessionCreator gameSessionCreator,
            BonusesRepository bonusesRepository,
            FurriesRepository furriesRepository,
            SessionRewardGiver rewardGiver,
            ScoreRepository scoreRepository,
            LeaderBoard leaderBoard,
            BackgroundVisual backgrounds)
        {
            _gameSessionCreator = gameSessionCreator;
            _bonusesRepository = bonusesRepository;
            _furriesRepository = furriesRepository;
            _scoreRepository = scoreRepository;

            _mainMenu.Init(gameSessionCreator, backgrounds);

            _sessionRewardGiver = rewardGiver;

            _leaderBoard = leaderBoard;

            _gameSessionCreator.Started += CreateSessionUI;
            _gameSessionCreator.Completed += ClearSessionCreator;
        }

        private void CreateSessionUI(IServiceLocator<ISessionService> sessionServiceLocator)
        {
            _currentSessionUI = Instantiate(_sessionUIPrefab, _canvasesParent);
            _currentSessionUI.Init(sessionServiceLocator, _bonusesRepository);
        }

        private void ClearSessionCreator(int finalScore)
        {
            Destroy(_currentSessionUI.gameObject);

            _currentFinalMenu = Instantiate(_finalMenu, _canvasesParent);
            _currentFinalMenu.Show(finalScore, _sessionRewardGiver, _scoreRepository, _leaderBoard);

            _currentFinalMenu.ReturningToMenu += ReturnToMenu;
            _currentFinalMenu.Restarting += Restart;
        }

        private void Restart()
        {
            _currentFinalMenu.ReturningToMenu -= ReturnToMenu;
            _currentFinalMenu.Restarting -= Restart;

            _currentFinalMenu.Close();
            _gameSessionCreator.Start();
        }

        private void ReturnToMenu()
        {
            _currentFinalMenu.ReturningToMenu -= ReturnToMenu;
            _currentFinalMenu.Restarting -= Restart;

            _currentFinalMenu.Close();
            _mainMenu.Show();
        }
    }
}