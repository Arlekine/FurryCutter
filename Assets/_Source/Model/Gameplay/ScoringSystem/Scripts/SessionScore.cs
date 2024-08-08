using System;

namespace FurryCutter.Gameplay.ScoringSystem
{
    public class SessionScore : IScore
    {
        public event Action<int> Updated;
        public int CurrentScore { get; private set; }

        public void Add(int value)
        {
            if (value <= 0)
                throw new ArgumentException($"{nameof(value)} should be positive");

            CurrentScore += value;
            Updated?.Invoke(CurrentScore);
        }
    }
}