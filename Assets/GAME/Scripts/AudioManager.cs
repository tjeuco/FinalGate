using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource effectAudioSource;

    [Header("Audio Clip")]
    [SerializeField] private AudioClip backgroundClip;
    [SerializeField] private AudioClip bulletClip;
    [SerializeField] private AudioClip jumpClip;

    void Start()
    {
        PlayBackgroundMusic();
    }
    public void PlayBackgroundMusic()
    {
        backgroundAudioSource.clip = backgroundClip;
        backgroundAudioSource.Play();
    }
    public void PlayBulletMusic()
    {
        effectAudioSource.PlayOneShot(bulletClip); 
    }

    public void PlayJumpMusic()
    {
        effectAudioSource.PlayOneShot(jumpClip);
    }
}
