using System;using DG.Tweening;
using FurryCutter.Application.Repositories;
using FurryCutter.Meta;
using GenericTweenAnimation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace FurryCutter.Presentation.UI.SessionFinal
{
    public class SessionFinalMenu : MonoBehaviour
    {
        private const string BestScoreFormat = "Best: 0}";
        private const string NewBest = "New best!";
        private const string LeaderBoardWaitText = "Getting leader board data...";
        private const string LeaderBoardFormat = "You place in the world is {0}!";

        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private CoinsRewardView _coinsView;

        [Space]
        [SerializeField] private TMP_Text _bestText;
        [SerializeField] private TMP_Text _leaderBoardText;

        [Space]
        [SerializeField] private Button _adButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private UIShowingAnimation _adButtonState;

        [Header("Animation")]
        [SerializeField] private UIShowingAnimation _initialPartsAnimation;
        [SerializeField] private UIShowingAnimation[] _finalPartsAnimations;
        [SerializeField] private float _delayBetweenAnimationsStart = 0.1f;
        [SerializeField] private UIShowingAnimation _hideAnimation;

        private SessionRewardGiver _sessionRewardGiver;
        private ScoreRepository _scoreRepository;
        private LeaderBoard _leaderBoard;
        private int _lastScore;
        private bool _isRewardDoubled;

        public event Action ReturningToMenu;
        public event Action Restarting;

        public void Show(int lastScore, SessionRewardGiver rewardGiver, ScoreRepository scoreRepository, LeaderBoard leaderBoard)
        {
            _sessionRewardGiver = rewardGiver;
            _lastScore = lastScore;
            _scoreRepository = scoreRepository;

            _bestText.text = _scoreRepository.IsLastScoreNewRecord ? NewBest : _scoreRepository.CurrentRecord.ToString(BestScoreFormat);
            
            _leaderBoard = leaderBoard;

            if (_leaderBoard.IsDataLoaded())
            {
                UpdateLeaderBoardRank(_leaderBoard.GetPlayerRank());
            }
            else
            {
                _leaderBoard.RankUpdated += UpdateLeaderBoardRank;
                _leaderBoardText.text = LeaderBoardWaitText;
            }

            Prewarm();
            
            _initialPartsAnimation.Show();
            _coinsView.Set(_sessionRewardGiver.GetCoinsForScore(lastScore, false));

            _scoreView.Show(lastScore, ContinueAnimation);
        }

        [EditorButton]
        public void ShowTest()
        {
            _bestText.text = (1000).ToString(BestScoreFormat);

            Prewarm();

            _initialPartsAnimation.Show();
            _coinsView.Set(100);

            _scoreView.Show(1000, ContinueAnimation);
        }

        [EditorButton]
        public void Close()
        {
            _hideAnimation.Hide();
            Destroy(gameObject, 0.5f);
        }

        private void OnEnable()
        {
            _adButton.onClick.AddListener(OnAdClick);
            _menuButton.onClick.AddListener(OnMenuClick);
            _restartButton.onClick.AddListener(OnRestartClick);

            YandexGame.RewardVideoEvent += OnAdShowed;
        }

        private void OnDisable()
        {
            _adButton.onClick.RemoveListener(OnAdClick);
            _menuButton.onClick.RemoveListener(OnMenuClick);
            _restartButton.onClick.RemoveListener(OnRestartClick);
            
            YandexGame.RewardVideoEvent -= OnAdShowed;
            _leaderBoard.RankUpdated -= UpdateLeaderBoardRank;
        }

        private void Prewarm()
        {
            _initialPartsAnimation.HideInstantly();

            foreach (var anim in _finalPartsAnimations)
            {
                anim.HideInstantly();
            }

            _scoreView.Prewarm();
            _coinsView.Prewarm(); 
            
            _adButtonState.ShowInstantly();
            _hideAnimation.ShowInstantly();
        }

        private void UpdateLeaderBoardRank(int rank)
        {
            if (rank == 0)
                _leaderBoardText.text = "";
            else
                _leaderBoardText.text = String.Format(LeaderBoardFormat, rank);
        }

        private void OnAdClick()
        {
            //TODO: show pop up
            YandexGame.RewVideoShow((int)RewardedAdType.DoubleCoins);
        }

        private void OnMenuClick()
        {
            _sessionRewardGiver.GiveReward(_lastScore, _isRewardDoubled);
            ReturningToMenu?.Invoke();
        }

        private void OnRestartClick()
        {
            _sessionRewardGiver.GiveReward(_lastScore, _isRewardDoubled);
            Restarting?.Invoke();
        }

        private void OnAdShowed(int id)
        {
            if ((RewardedAdType)id == RewardedAdType.DoubleCoins)
            {
                _adButton.interactable = false;
                _adButtonState.Hide();

                 _isRewardDoubled = true;
                var coins = _sessionRewardGiver.GetCoinsForScore(_lastScore, _isRewardDoubled);
                _coinsView.Select(coins);
            }
        }

        private void ContinueAnimation()
        {
            for (int i = 0; i < _finalPartsAnimations.Length; i++)
            {
                _finalPartsAnimations[i].Show().SetDelay(_delayBetweenAnimationsStart * i);
            }
        }
    }
}
