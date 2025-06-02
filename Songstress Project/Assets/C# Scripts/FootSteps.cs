using UnityEngine;

public class FootSteps : MonoBehaviour
{

    [SerializeField] private AudioClip[] footSteps = new AudioClip[6];
    private static FootSteps instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public static void PlaySound(int randStep, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.footSteps[randStep], volume);
    }


}
