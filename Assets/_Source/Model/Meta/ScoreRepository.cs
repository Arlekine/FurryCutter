using System;
using DataSystem;
using FurryCutter.Gameplay.Session.ServiceLocator;
using FurryCutter.Meta;

namespace FurryCutter.Application.Repositories
{
    public class ScoreRepository : IDisposable
    {
        protected DataContext _dataContext;

        private IGameSessionCreator _gameSessionCreator;
        private LeaderBoard _leaderBoard;

        private bool _isLastScoreNewRecord;

        public ScoreRepository(DataContext dataContext, LeaderBoard leaderBoard, IGameSessionCreator gameSessionCreator)
        {
            _dataContext = dataContext;
            _gameSessionCreator = gameSessionCreator;
            _leaderBoard = leaderBoard;

            _gameSessionCreator.Completed += OnNewScore;
        }

        public int CurrentRecord => _dataContext.CurrentGameData.Record;
        public bool IsLastScoreNewRecord => _isLastScoreNewRecord;

        public void Dispose()
        {
            _gameSessionCreator.Completed -= OnNewScore;
        }

        private void OnNewScore(int score)
        {
            if (_dataContext.CurrentGameData.Record < score)
            {
                _isLastScoreNewRecord = true;
                _dataContext.CurrentGameData.Record = score;
                _leaderBoard.SetNewRecord(score);

                _dataContext.MarkDirty();
            }
            else
            {
                _isLastScoreNewRecord = false;
            }
        }
    }
}