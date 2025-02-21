using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent _game_event;
    [SerializeField] private UnityEvent _respone;

    private void OnEnable()
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
