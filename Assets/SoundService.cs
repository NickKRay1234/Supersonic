using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundService : MonoBehaviour, ISoundService
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        ServiceLocator.RegisterService(this);
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        audioSource.clip = clip;
        audioSource.Play();
    }
}