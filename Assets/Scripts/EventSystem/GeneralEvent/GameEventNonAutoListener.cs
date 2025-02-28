using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventNonAutoListener : MonoBehaviour
{
    [SerializeField] private GameEvent _game_event;
    [SerializeField] private UnityEvent _respone;

    public void ListenEvent()
    {
        _game_event?.ListenEvent(Respone);
    }
    private void OnDisable()
    {
        _game_event?.UnListenEvent(Respone);
    }
    private void Respone()
    {
        _respone?.Invoke();
    }
}
