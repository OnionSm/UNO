using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

public class CardEventListener : MonoBehaviour
{
    [SerializeField] private CardEvent _game_event;
    [SerializeField] private UnityEvent<GameObject, CardColor, CardType, CardSymbol> _respones;
    private void OnEnable()
    {
        _game_event?.ListenEvent(Respone);
    }
    private void OnDisable()
    {
        _game_event?.UnlistenEvent(Respone);
    }
    private void Respone(GameObject card, CardColor color , CardType type , CardSymbol symbol)
    {
        _respones?.Invoke(card, color, type, symbol);
    }
}
