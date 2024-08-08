using System;
using System.Collections;
using UnityEngine;

public static class MonoBehExtensions
{
    public static Coroutine ActionAfterPause(this MonoBehaviour context, float pause, Action action)
    {
        return context.StartCoroutine(ActionAfterPauseRoutine(pause, action));
    }

    public static Coroutine ActionNextFrame(this MonoBehaviour context, Action action)
    {
        return context.StartCoroutine(ActionNextFrameRoutine(action));
    }

    private static IEnumerator ActionNextFrameRoutine(Action action)
    {
        yield return null;
        action?.Invoke();
    }

    private static IEnumerator ActionAfterPauseRoutine(float pause, Action action)
    {
        yield return new WaitForSeconds(pause);
        action?.Invoke();
    }
}