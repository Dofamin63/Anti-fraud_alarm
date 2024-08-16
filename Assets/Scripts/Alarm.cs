using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private int _speedChangeVolume;
    [SerializeField] private int _minVolume;
    [SerializeField] private int _maxVolume;
    private AudioSource _audioSource;
    private Coroutine _coroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy _))
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(ChangeVolume());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy _))
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(ChangeVolume());
        }
    }

    private IEnumerator ChangeVolume()
    {
        int targetVolume = _minVolume;
        _audioSource.Play();

        if (_audioSource.volume == _minVolume)
        {
            targetVolume = _maxVolume;
        }
        else if (_audioSource.volume == _maxVolume)
        {
            targetVolume = _minVolume;
        }

        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _speedChangeVolume * Time.deltaTime);
            yield return null;
        }
        
        _audioSource.Stop();
    }
}