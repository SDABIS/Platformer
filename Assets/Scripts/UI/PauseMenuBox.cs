using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PauseMenuBox : MonoBehaviour
{
    public PlayerController player;
    protected UnityEvent updateEvent;

    protected virtual void OnEnable() {
        if(player != null) DisplayInfo();
        updateEvent.AddListener(DisplayInfo);
    }

    public abstract void DisplayInfo();

    protected virtual void OnDisable() {
        updateEvent.RemoveListener(DisplayInfo);
    }
}