using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[Serializable]
public class GameEventListener : MonoBehaviour {

    public GameEvent myGameEvent;
    public UnityEvent myTriggeredEvent;

    private void OnEnable()
    {
        if (myTriggeredEvent == null)
        {
            Debug.LogError("No triggered event assigned on " + this.gameObject.name);
        }
        if (myGameEvent == null)
        {
            Debug.LogError("No game event assigned on " + this.gameObject.name);
        }
        myGameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (myTriggeredEvent == null)
        {
            Debug.LogError("No triggered event assigned on " + this.gameObject.name);
        }
        if (myGameEvent == null)
        {
            Debug.LogError("No game event assigned on " + this.gameObject.name);
        }
        myGameEvent.UnregisterListener(this);
    }

    public void Raise()
    {
        myTriggeredEvent.Invoke();
    }
}
