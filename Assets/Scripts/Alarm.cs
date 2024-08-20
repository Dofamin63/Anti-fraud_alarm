using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private int _speedChangeVolume;
    [SerializeField] private float _minVolume;
    [SerializeField] private float _maxVolume;
    private AudioSource _audioSource;
    private Coroutine _coroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void TurnOnSiren()
    {
        _audioSource.Play();
        OnChangeVolume(_maxVolume);
    }
    
    public void TurnOffSiren()
    {
        OnChangeVolume(_minVolume);
    }

    private void OnChangeVolume(float targetVolume)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeVolume(targetVolume));
    }
    
    private IEnumerator ChangeVolume(float targetVolume)
    {
        while (Mathf.Approximately(_audioSource.volume, targetVolume) == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _speedChangeVolume * Time.deltaTime);
            
            yield return null;
        }

        if (Mathf.Approximately(_audioSource.volume, _minVolume))
            _audioSource.Stop();
    }
}