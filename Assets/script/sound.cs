using UnityEngine;

public class Sound : MonoBehaviour
{
    private AudioSource[] audioSources;
    private float[] baseVolumes;

    private void Awake()
    {
        audioSources = GetComponentsInChildren<AudioSource>();
    }

    public void AudioOn()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = baseVolumes[i];
        }
    }

    public void AudioOff()
    {
        foreach (AudioSource source in audioSources)
        {
            source.volume = 0;
        }
    }

    public void Start()
    {
        baseVolumes = new float[audioSources.Length];

        for (int i = 0; i < audioSources.Length; i++)
        {
            baseVolumes[i] = audioSources[i].volume;
        }
    }
}



