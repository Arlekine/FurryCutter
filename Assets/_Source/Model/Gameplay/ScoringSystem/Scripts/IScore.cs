using System;
using FurryCutter.Gameplay.Session.ServiceLocator;

namespace FurryCutter.Gameplay.ScoringSystem
{
    public interface IScore : ISessionService
    {
        event Action<int> Updated; 
        int CurrentScore { get; }
        void Add(int value);
    }
}