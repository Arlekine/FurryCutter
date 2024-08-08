using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class TypedTrigger<T> : MonoBehaviour
{
    public event Action<T> TriggerEnter;
    public event Action<T> TriggerExit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var targetComponent = other.GetComponent<T>();

        if (targetComponent != null)
        {
            TriggerEnter?.Invoke(targetComponent);
            OnEnterTriggered(targetComponent);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var targetComponent = other.GetComponent<T>();

        if (targetComponent != null)
        {
            TriggerExit?.Invoke(targetComponent);
            OnExitTriggered(targetComponent);
        }
    }

    protected virtual void OnEnterTriggered(T other)
    { }

    protected virtual void OnExitTriggered(T other)
    { }

    protected virtual void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}