using System.Collections;
using UnityEngine;

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

    public void TurnOnSiren()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _audioSource.Play();
        _coroutine = StartCoroutine(ChangeVolumeMax());
    }
    
    public void TurnOffSiren()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeVolumeMin());
    }
    
    private IEnumerator ChangeVolumeMax()
    {
        _audioSource.volume = _minVolume;

        while (_audioSource.volume < _maxVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _speedChangeVolume * Time.deltaTime);
            
            yield return null;
        }
    }
    
    private IEnumerator ChangeVolumeMin()
    {
        while (_audioSource.volume > _minVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _speedChangeVolume * Time.deltaTime);
            
            yield return null;
        }
        
        _audioSource.Stop();
    }
}