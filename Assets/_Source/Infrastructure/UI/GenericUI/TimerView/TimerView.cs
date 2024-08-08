using System.Collections;
using UnityEngine;

namespace GenericUI
{
    public abstract class TimerView : MonoBehaviour
    {
        protected Timer _timer;
        private Coroutine _timerRoutine;

        public virtual void SetTimer(Timer timer)
        {
            _timer = timer;
            _timer.Started += OnTimerStarted;
            _timer.Expired += OnTimerExpired;

            if (_timer.IsRunning)
                OnTimerStarted();
        }

        protected virtual void OnTimerStarted()
        {
            _timerRoutine = StartCoroutine(TimerRoutine());
        }

        protected virtual void OnTimerExpired()
        {
            if (_timerRoutine != null)
            {
                StopCoroutine(_timerRoutine);
                _timerRoutine = null;
            }
        }

        protected abstract void UpdateView();
        
        private IEnumerator TimerRoutine()
        {
            while (true)
            {
                yield return null;
                UpdateView();
            }
        }

        private void OnDestroy()
        {
            if (_timer != null)
            {
                _timer.Started -= OnTimerStarted;
                _timer.Expired -= OnTimerExpired;
            }
        }
    }
}