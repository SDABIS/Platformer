using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBroker : Singleton<EventBroker> {
    [System.Serializable] public class EventEnemyKill : UnityEvent<int> { }
    [System.Serializable] public class EventStatChange : UnityEvent { }
    [System.Serializable] public class EventEquipChange : UnityEvent { }
    [System.Serializable] public class EventInventoryChange : UnityEvent { }

    public EventEnemyKill OnEnemyKill;
    public EventStatChange OnStatChange;
    public EventEquipChange OnEquipChange;
    public EventInventoryChange OnInventoryChange;

    public void CallEnemyKill(int exp) {
        OnEnemyKill.Invoke(exp);
    }

    public void CallStatChange() {
        OnStatChange.Invoke();
    }

    public void CallEquipChange() {
        OnEquipChange.Invoke();
        OnStatChange.Invoke();
    }

    public void CallInventoryChange() {
        OnInventoryChange.Invoke();
    }

}