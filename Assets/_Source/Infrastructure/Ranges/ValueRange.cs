using System;
using Random = UnityEngine.Random;

public struct ValueRange
{
    private float _min;
    private float _max;

    public ValueRange(float min, float max)
    {
        if (min >= max)
            throw new ArgumentException($"{nameof(min)} should be less than {nameof(max)}");

        _min = min;
        _max = max;
    }

    public float Min => _min;
    public float Max => _max;

    public float GetRandom() => Random.Range(Min, Max);
}
