using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTurnEvent : ScriptableObject
{
    private UnityAction<CardColor, CardType, CardNumber> _all_event;
    public void ListenEvent(UnityAction<CardColor, CardType, CardNumber> action)
    {
        _all_event += action;
    }
    public void UnlistenEvent(UnityAction<CardColor, CardType, CardNumber> action)
    {
        _all_event -= action;
    }
    public void RaiseEvent(CardColor color , CardType type, CardNumber number)
    {
        _all_event?.Invoke(color, type, number);
    }
}
