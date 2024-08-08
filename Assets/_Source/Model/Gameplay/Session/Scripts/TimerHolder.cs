using FurryCutter.Gameplay.Session.ServiceLocator;

namespace FurryCutter.Gameplay.Session
{
    public class TimerHolder : ISessionService
    {
        private Timer _timer;

        public Timer Timer => _timer;

        public TimerHolder(Timer timer)
        {
            _timer = timer;
        }
    }
}