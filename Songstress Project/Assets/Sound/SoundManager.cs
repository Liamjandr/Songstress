using UnityEngine;


public enum Octave{
    C5,
    D5,
    E5,
    F5,
    G5,
    A5,
    B5,
    C6,
    D6,
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] kalimbaClips = new AudioClip[9];
    [SerializeField] private AudioClip[] eguitarClips = new AudioClip[9];
    [SerializeField] private AudioClip[] guitarClips = new AudioClip[9];
    [SerializeField] private AudioClip[] saxClips = new AudioClip[9];
    [SerializeField] private AudioClip[] harmonicaClips = new AudioClip[9];
    [SerializeField] private AudioClip[] djembeClips = new AudioClip[9];

    [SerializeField] private AudioClip[] trumpetClips = new AudioClip[9];

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

    public static void PlayInstrument(int instrument, Octave clip, float volume = 1)
    {
        switch (instrument) 
        { 
        case 0: instance.audioSource.PlayOneShot(instance.kalimbaClips[(int)clip], volume); break;
        case 1: instance.audioSource.PlayOneShot(instance.eguitarClips[(int)clip], volume); break;
        case 2: instance.audioSource.PlayOneShot(instance.guitarClips[(int)clip], volume); break;
        case 3: instance.audioSource.PlayOneShot(instance.saxClips[(int)clip], volume); break;
        case 4: instance.audioSource.PlayOneShot(instance.harmonicaClips[(int)clip], volume); break;
        case 5: instance.audioSource.PlayOneShot(instance.djembeClips[(int)clip], volume); break;

        case 10: instance.audioSource.PlayOneShot(instance.trumpetClips[(int)clip], volume); break;
        default: break;
        }
    }

    public static void PlayInstrument(Octave kalvfx, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.kalimbaClips[(int)kalvfx], volume);
    }


}
