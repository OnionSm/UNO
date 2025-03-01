using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "CardEvent", menuName = "Event/CardEvent")]
public class CardEvent : ScriptableObject
{
    public UnityAction<GameObject, CardColor, CardType, CardSymbol> _all_event;
    public void ListenEvent(UnityAction<GameObject, CardColor, CardType, CardSymbol> action)
    {
        _all_event += action;
    }
    public void UnlistenEvent(UnityAction<GameObject, CardColor, CardType, CardSymbol> action)
    {
        _all_event -= action;
    }
    public void RaiseEvent(GameObject card, CardColor color , CardType type, CardSymbol symbol)
    {
        _all_event?.Invoke(card, color, type, symbol);
    }
}
