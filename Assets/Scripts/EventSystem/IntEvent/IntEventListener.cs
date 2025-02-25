using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeTurnEventListener : MonoBehaviour
{
    [SerializeField] private IntEvent _game_event;
    [SerializeField] private UnityEvent<int> _respones;
    private void OnEnable()
    {
        _game_event?.ListenEvent(Respone);
    }
    private void OnDisable()
    {
        _game_event?.UnListenEvent(Respone);
    }
    private void Respone(int current_turn)
    {
        _respones?.Invoke(current_turn);
    }
}
