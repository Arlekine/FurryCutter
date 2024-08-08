using FurryCutter.Application.Repositories;
using FurryCutter.Bonuses;
using FurryCutter.Gameplay.ScoringSystem;
using FurryCutter.Gameplay.Session;
using FurryCutter.Gameplay.Session.ServiceLocator;
using GenericUI;
using UnityEngine;

namespace FurryCutter.Presentation.UI
{
    public class SessionUI : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TimerView _timerView;
        [SerializeField] private ScoringSystemPresenter _scoringSystemPresenter;
        [SerializeField] private BonusMenuPresenter _bonusMenuPresenter;

        [Header("Final countdown")]
        [SerializeField] private int _secondsToStartFinalCountDown = 5;
        [SerializeField] private CountDownPopUpTimer _countDownTimer;

        private Timer _timer;

        public void Init(IServiceLocator<ISessionService> gameSessionLocator, BonusesRepository bonusRepository)
        {
            _canvas.worldCamera = Camera.main;
            _timer = gameSessionLocator.GetService<TimerHolder>().Timer;
            _timer.TimeUpdated += OnTimerUpdated;

            _timerView.SetTimer(_timer);

            _scoringSystemPresenter.Init(gameSessionLocator.GetService<IComboSystem>(), gameSessionLocator.GetService<IScore>());
            _bonusMenuPresenter.Init(bonusRepository.GetAll(), gameSessionLocator.GetService<IBonusSystem>(), gameSessionLocator.GetService<SessionScene>().SceneSessionParent);
        }

        private void OnTimerUpdated(float secondsPass)
        {
            if (_timer.TimeLeft.Seconds < _secondsToStartFinalCountDown)
                _countDownTimer.SetTimer(_timer);
        }

        private void OnDestroy()
        {
            _timer.TimeUpdated -= OnTimerUpdated;
        }
    }
}
