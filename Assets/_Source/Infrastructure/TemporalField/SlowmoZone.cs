using System;
using System.Collections.Generic;
using UnityEngine;

public class SlowmoZone : TypedTrigger<ISlowable>
{
    [Range(0f, 1f)][SerializeField] private float _slowSpeed = 0.2f;

    private Dictionary<ISlowable, float> _slowablesInZones = new Dictionary<ISlowable, float>();

    protected override void OnEnterTriggered(ISlowable other)
    {
        if (_slowablesInZones.ContainsKey(other))
            throw new ArgumentException($"Attempt to add slowable that is already in field");

        _slowablesInZones.Add(other, other.CurrentNormalizedSpeed);
        other.SetNormalizedSpeed(_slowSpeed);
    }

    protected override void OnExitTriggered(ISlowable other)
    {
        if (_slowablesInZones.ContainsKey(other))
        {
            other.SetNormalizedSpeed(_slowablesInZones[other]);
            _slowablesInZones.Remove(other);
        }
    }
}
