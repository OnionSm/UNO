using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BoolEvent", menuName = "Event/BoolEvent")]
public class BoolEvent : ScriptableObject
{
    private UnityAction<bool> _all_events;
    
    public void ListenEvent(UnityAction<bool> action)
    {
        _all_events += action;
    }
    public void UnListenEvent(UnityAction<bool> action)
    {
        _all_events -= action;
    }
    public void RaiseEvent(bool value)
    {
        _all_events?.Invoke(value);
    }
}
