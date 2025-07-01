using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloatEventListener : MonoBehaviour
{
    [SerializeField] private FloatEvent _event;
    [SerializeField] private UnityEvent<float> _respones;

    private void OnEnable()
    {
        _event.ListenEvent(Respone);
    }

    private void OnDisable()
    {
        _event.UnListenEvent(Respone);
    }

    private void Respone(float value)
    {
        _respones?.Invoke(value);
    }
}
