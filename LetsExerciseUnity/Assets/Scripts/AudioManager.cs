using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------AudioSource-------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource voiceSource;

    [Header("-------AudioClip-------")]
    public AudioClip BGMStart;
    public AudioClip buttonClick;
    public AudioClip trainerIntroduce;
    public AudioClip askIfNewUser;
    public AudioClip selectOldUser;
    public List<AudioClip> cheerUp;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = BGMStart;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void TrainerSpeak(AudioClip clip)
    {
        voiceSource.PlayOneShot(clip);
    }
}
