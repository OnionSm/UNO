using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FloatEvent", menuName = "Event/FloatEvent")]
public class FloatEvent : ScriptableObject
{
    public UnityAction<float> _all_event;

    public void ListenEvent(UnityAction<float> action)
    {
        _all_event += action;
    }

    public void UnListenEvent(UnityAction<float> action)
    {
        _all_event -= action;
    }

    public void RaiseEvent(float value)
    {
        _all_event?.Invoke(value);
    }
}
