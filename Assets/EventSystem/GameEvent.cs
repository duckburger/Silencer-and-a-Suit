using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName = "SAAS/GameEvent")]
public class GameEvent : ScriptableObject {

    public List<GameEventListener> listeners = new List<GameEventListener>();


    public void Invoke()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].Raise();
        }
    }


    public void RegisterListener(GameEventListener newListener)
    {
        if (!listeners.Contains(newListener))
        {
            listeners.Add(newListener);
        }
    }

    public void UnregisterListener(GameEventListener listenerToRemove)
    {
        if (listeners.Contains(listenerToRemove))
        {
            listeners.Remove(listenerToRemove);
        }
    }
}
