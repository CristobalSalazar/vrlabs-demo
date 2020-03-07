using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float time { get; private set; }
    private const float IDLE_TIME_BEFORE_WARNING = 15;

    private Dictionary<Action, float> dict = new Dictionary<Action, float>();

    public void On(float time, Action action)
    {
        dict.Add(action, time);
    }


    public void ClearEvents()
    {
        dict.Clear();
    }


    private void TimeActions()
    {
        if (dict.Keys.Count == 0) return;

        List<Action> keys = new List<Action>();
        foreach (var entry in dict)
        {
            if (time >= entry.Value)
            {
                entry.Key?.Invoke();
                keys.Add(entry.Key);
            }
        }
        RemoveKeys(keys);
    }

    private void RemoveKeys(List<Action> keys)
    {
        foreach (var key in keys)
        {
            dict.Remove(key);
        }
    }

    private IEnumerator Run()
    {
        while (true)
        {
            time += Time.deltaTime;
            TimeActions();
            yield return null;
        }
    }

    public void ResetTimer()
    {
        time = 0;
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }

    public void StartTimer()
    {
        StartCoroutine(nameof(Run));
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }
}