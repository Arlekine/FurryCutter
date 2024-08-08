using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    private float _currentTargetTime;
    private float _currentTime;
    private bool _isPaused;

    private MonoBehaviour _context;
    private Coroutine _currentTimer;

    public event Action<float> TimeUpdated;
    public event Action Started;
    public event Action Expired;

    public bool IsRunning => _currentTimer != null;
    public TimeSpan TimeLeft => TimeSpan.FromSeconds((double)(_currentTargetTime - _currentTime));
    public TimeSpan TimePass => TimeSpan.FromSeconds((double)_currentTime);
    public float TimeLeftNormalized => (_currentTargetTime - (float)TimePass.TotalSeconds) / _currentTargetTime;
    public float TimePassNormalized => ((float)TimePass.TotalSeconds) / _currentTargetTime;

    public Timer(MonoBehaviour context)
    {
        _context = context;
    }

    public void StartTimer(int seconds)
    {
        if (_currentTimer != null)
            throw new ArgumentException("Can't start timer while it is running. Stop timer before start it again.");

        _currentTargetTime = seconds;
        _currentTime = 0f;
        _isPaused = false;

        _currentTimer = _context.StartCoroutine(TimerRoutine());
        Started?.Invoke();
    }

    public void Stop()
    {
        _context.StopCoroutine(_currentTimer);
        _currentTimer = null;

        _currentTime = 0f;
        _currentTargetTime = 0f;
    }

    public void Pause()
    {
        _isPaused = true;
    }

    public void Continue()
    {
        _isPaused = false;
    }

    private IEnumerator TimerRoutine()
    {
        while (_isPaused == false)
        {
            _currentTime += Time.deltaTime;
            TimeUpdated?.Invoke(_currentTime);

            if (_currentTime >= _currentTargetTime)
            {
                _isPaused = false;
                Expired?.Invoke();
                Stop();
            }

            yield return null;
        }
    }
}