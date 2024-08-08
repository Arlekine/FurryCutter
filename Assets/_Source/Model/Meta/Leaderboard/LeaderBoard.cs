using System;
using UnityEngine;
using YG;
using YG.Utils.LB;

namespace FurryCutter.Meta
{
    public class LeaderBoard : IDisposable
    {
        private string _leaderBoardName;
        private LBData _currentData;

        public event Action<int> RankUpdated; 

        public LeaderBoard(string leaderBoardName)
        {
            _leaderBoardName = leaderBoardName;

            YandexGame.onGetLeaderboard += OnGotLeaderBoard;
            YandexGame.GetLeaderboard(_leaderBoardName, 5, 1, 1, "");
        }

        public bool IsDataLoaded() => _currentData != null && _currentData.thisPlayer != null;

        public int GetPlayerRank() => _currentData.thisPlayer.rank;

        public void SetNewRecord(int value)
        {
            _currentData = null;
            YandexGame.NewLeaderboardScores(_leaderBoardName, value);
            YandexGame.GetLeaderboard(_leaderBoardName, 5, 1, 1, "");
        }

        public void Dispose()
        {
            YandexGame.onGetLeaderboard -= OnGotLeaderBoard;
        }

        private void OnGotLeaderBoard(LBData data)
        {
            _currentData = data;
            RankUpdated?.Invoke(_currentData.thisPlayer.rank);
        }
    }
}
