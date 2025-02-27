using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "IntEvent", menuName = "Event/IntEvent")]
public class IntEvent : ScriptableObject
{
    public UnityAction<int> _all_action;
    public void ListenEvent(UnityAction<int> action)
    {
        _all_action += action;
    }
    public void UnListenEvent(UnityAction<int> action)
    {
        _all_action -= action;
    }
    public void RaiseEvent(int current_turn)
    {
        _all_action?.Invoke(current_turn);
    }
}
