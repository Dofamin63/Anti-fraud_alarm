using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;

    public event Action OnDetection;

    private void Awake()
    {
        _alarm = GetComponent<Alarm>();
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleTrigger(other);
    }

    private void OnTriggerExit(Collider other)
    {
        HandleTrigger(other);
    }

    private void HandleTrigger(Collider other)
    {
        if (other.TryGetComponent(out Enemy _))
        {
            OnDetection?.Invoke();
        }
    }
}