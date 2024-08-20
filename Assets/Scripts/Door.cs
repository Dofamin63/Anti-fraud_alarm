using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;

    private void Awake()
    {
        _alarm = GetComponent<Alarm>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy _))
        {
            _alarm.TurnOnSiren();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy _))
        {
            _alarm.TurnOffSiren();
        }
    }
}