using System;

namespace FurryCutter.Gameplay.Session.ServiceLocator
{
    public interface IGameSessionCreator
    {
        event Action<IServiceLocator<ISessionService>> Started;
        event Action<int> Completed;

        IServiceLocator<ISessionService> CurrentServiceLocator { get; }
        void Start();
        void Pause();
        void Continue();
    }
}