using System;
using System.Collections.Generic;

public static class SimulationEvents
{
    private static readonly Dictionary<string, Action> events = new Dictionary<string, Action>();
    private static readonly Dictionary<EventType, Action> simEvents = new Dictionary<EventType, Action>();

    public static void On(EventType e, Action action)
    {
        if (simEvents.ContainsKey(e))
        {
            simEvents[e] += action;
        }
        else
        {
            simEvents.Add(e, action);
        }
    }

    public static void On(string name, Action action)
    {
        if (events.ContainsKey(name))
        {
            events[name] += action;
        }
        else
        {
            events.Add(name, action);
        }
    }

    public static void Unsubscribe(EventType e, Action action)
    {
        if (simEvents.ContainsKey(e))
        {
            simEvents[e] -= action;
        }
    }

    public static void Unsubscribe(string eventName, Action action)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName] -= action;
        }
    }

    public static void Emit(string eventName)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName]?.Invoke();
        }
    }

    public static void Emit(EventType e)
    {
        if (simEvents.ContainsKey(e))
        {
            simEvents[e]?.Invoke();
        }
    }
}