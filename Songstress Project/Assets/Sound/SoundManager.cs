using UnityEngine;


public enum Kalimba{
    kal_C5,
    kal_D5,
    kal_E5,
    kal_F5,
    kal_G5,
    kal_A5,
    kal_B5,
    kal_C6,
    kal_D6,
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] kalimbaClips;

    private static SoundManager instance;
    private AudioSource audioSource;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlayKalimba(Kalimba kalvfx, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.kalimbaClips[(int)kalvfx], volume);
    }
}
