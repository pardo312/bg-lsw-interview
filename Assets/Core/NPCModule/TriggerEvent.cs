using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent<Collider2D> onTriggerEnterEvent;
    [SerializeField] private UnityEvent<Collider2D> onTriggerExitEvent;

    public Action<Collider2D> onTriggerEnter;
    public Action<Collider2D> onTriggerExit;


    private void OnTriggerEnter2D(Collider2D other)
    {
        onTriggerEnter?.Invoke(other);
        onTriggerEnterEvent?.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        onTriggerExit?.Invoke(other);
        onTriggerExitEvent?.Invoke(other);
    }
}