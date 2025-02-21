using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEvent", menuName ="Event/GameEvent")]
public class GameEvent : ScriptableObject
{
    public UnityAction EventAction;
    public void ListenEvent(UnityAction action)
    {
        EventAction += action;
    }
    public void UnListenEvent(UnityAction action)
    {
        EventAction -= action; 
    }
    public void RaiseEvent()
    {
        EventAction?.Invoke();
    }
}
