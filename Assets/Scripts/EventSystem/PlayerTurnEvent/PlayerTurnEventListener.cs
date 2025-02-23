using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTurnEventListener : MonoBehaviour
{
    [SerializeField] private PlayerTurnEvent _game_event;
    [SerializeField] private UnityEvent _respones;
    private void OnEnable()
    {
        _game_event?.ListenEvent(Respone);
    }
    private void Respone(CardColor color , CardType type , CardSymbol symbol)
    {

    }
}
