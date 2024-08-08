using System;
using Random = UnityEngine.Random;

public class RandomIntervalTrigger : ITimeTrigger, IDisposable
{
    public event Action Triggered;
    
    private float _minInterval;
    private float _maxInterval;
    private Timer _timer;

    private float _nextUpdateTime;

    public RandomIntervalTrigger(float minInterval, float maxInterval, Timer timer)
    {
        if (minInterval >= maxInterval)
            throw new ArgumentException($"{nameof(minInterval)} should be less than {nameof(maxInterval)}");

        _minInterval = minInterval;
        _maxInterval = maxInterval;
        _timer = timer;

        _timer.TimeUpdated += OnTimerUpdated;
        UpdateNextTime();
    }

    public void Dispose()
    {
        _timer.TimeUpdated -= OnTimerUpdated;
    }

    private void OnTimerUpdated(float currentTime)
    {
        if (currentTime >= _nextUpdateTime)
        {
            Triggered?.Invoke();
            UpdateNextTime();
        }
    }

    private void UpdateNextTime()
    {
        _nextUpdateTime += Random.Range(_minInterval, _maxInterval);
    }
}