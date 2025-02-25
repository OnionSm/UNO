using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoolEventListener : MonoBehaviour
{
    [SerializeField] private BoolEvent _event;
    [SerializeField] private UnityEvent<bool> _respones;

    private void OnEnable()
    {
        _event?.ListenEvent(Respone);
    }
    private void OnDisable()
    {
        _event?.UnListenEvent(Respone);
    }
    private void Respone(bool value)
    {
        _respones?.Invoke(value);
    }
}
