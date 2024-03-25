using UnityEngine;

public class sound : MonoBehaviour
{
    private AudioSource audioSource;
    public float baseVolume;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void AudioOn()
    {
        audioSource.volume = baseVolume;
    }
    public void AudioOff()
    {
        audioSource.volume = 0;
    }
    public void Start()
    {
        baseVolume = audioSource.volume;
    }
}



