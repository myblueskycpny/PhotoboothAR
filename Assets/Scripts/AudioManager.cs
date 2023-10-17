using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private Dictionary<string, AudioSource> _audioSources;

    private void Awake()
    {
        _audioSources = new Dictionary<string, AudioSource>();
    }

    public void AddAudioSource(string objectName, AudioSource audioSource)
    {
        _audioSources.Add(objectName, audioSource);
    }

    public void PlayAudio(string objectName)
    {
        if (_audioSources.ContainsKey(objectName))
        {
            AudioSource audioSource = _audioSources[objectName];
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    public void StopAudio(string objectName)
    {
        if (_audioSources.ContainsKey(objectName))
        {
            AudioSource audioSource = _audioSources[objectName];
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
