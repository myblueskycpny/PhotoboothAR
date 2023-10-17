using UnityEngine;

public class FindEnabledAudioSources : MonoBehaviour
{
    public AudioSource[] enabledAudioSources;

    void Start()
    {
        // Find all AudioSource components in the scene.
        AudioSource[] allAudioSources = GameObject.FindObjectsOfType<AudioSource>();

        // Filter out the disabled AudioSources.
        enabledAudioSources = new AudioSource[allAudioSources.Length];
        int enabledAudioSourceIndex = 0;
        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource.enabled)
            {
                enabledAudioSources[enabledAudioSourceIndex] = audioSource;
                enabledAudioSourceIndex++;
            }
        }
    }
}
