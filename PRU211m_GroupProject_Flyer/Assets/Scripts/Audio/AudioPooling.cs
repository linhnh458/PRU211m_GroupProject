using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPooling : MonoBehaviour
{
    public static AudioPooling audioInstance;
    [SerializeField] int poolSize = 5;
    [SerializeField] AudioSource audioSourcePrefab;
    private static List<AudioSource> poolAudioSources = new List<AudioSource>();

    private void Awake()
    {
        if (audioInstance == null)
        {
            audioInstance = this;
        }
    }
    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            AddAudioSourceToPool();
        }
    }

    private AudioSource AddAudioSourceToPool()
    {
        AudioSource newAudioSource = Instantiate(audioSourcePrefab);
        poolAudioSources.Add(newAudioSource);
        return newAudioSource;
    }

    // get and play the first available audio
    public AudioSource GetAudioSource()
    {
        foreach (AudioSource audioSource in poolAudioSources)
        {
            // if audio isnt playing(inactive) => available to use
            if (!audioSource.isPlaying)
            {
                return audioSource;
            }
        }
        // If no available audio source is found, create a new one and add it to the pool
        AudioSource newAudioSource = AddAudioSourceToPool();
        return newAudioSource;
    }

    // return audio source back to the pool
    public AudioSource ReturnAudioSource(AudioSource audioSource)
    {
        audioSource.clip = null;
        poolAudioSources.Add(audioSource);
        return audioSource;
    }

    public void PlaySound(AudioClip clip)
    {
        AudioSource audioSource = GetAudioSource();
        audioSource.clip = clip;
        audioSource.Play();
        StartCoroutine(DisableAudio(audioSource, clip.length));
    }

    public IEnumerator DisableAudio(AudioSource audioSource, float duration)
    {
        yield return new WaitForSeconds(duration);
        ReturnAudioSource(audioSource);
    }
}
