using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatcher : MonoBehaviour
{
    private static EventDispatcher instance;

    private Dictionary<GameEvent, Action<object>> listeners = new Dictionary<GameEvent, Action<object>>();

    public static EventDispatcher Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject eventDispatch = new GameObject("Event Dispatcher");
                EventDispatcher _instance = eventDispatch.AddComponent<EventDispatcher>();
                instance = _instance;
            }
            return instance;
        }
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (this != instance)
        {
            Destroy(gameObject);
        }
    }

    public void RegisterListener(GameEvent gameEvent, Action<object> callback)
    {
        if (listeners.ContainsKey(gameEvent))
        {
            listeners[gameEvent] += callback;
        }
        else
        {
            listeners.Add(gameEvent, null);
            listeners[gameEvent] += callback;
        }
    }

    public void PostEvent(GameEvent gameEvent, object param)
    {
        var callback = listeners[gameEvent];

        if (callback != null)
        {
            callback(param);
        }
    }

    public void RemoveListener(GameEvent gameEvent)
    {
        if(listeners.ContainsKey(gameEvent))
        {
            listeners[gameEvent] = null;
            listeners.Remove(gameEvent);
        }
    }
}

public static class EventDispatchExtension
{
    public static void RegisterListener(this MonoBehaviour listener, GameEvent gameEvent, Action<object> callback)
    {
        EventDispatcher.Instance.RegisterListener(gameEvent, callback);
    }
    public static void PostEvent(this MonoBehaviour listener, GameEvent gameEvent, object param)
    {
        EventDispatcher.Instance.PostEvent(gameEvent, param);
    }
    public static void RemoveListener(this MonoBehaviour listener, GameEvent gameEvent)
    {
        EventDispatcher.Instance.RemoveListener(gameEvent);
    }
}
