using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;

    public event Action Entered;
    public event Action Exited;

    private void Awake()
    {
        _alarm = GetComponent<Alarm>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy _))
        {
            Entered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy _))
        {
            Exited?.Invoke();
        }
    }
}