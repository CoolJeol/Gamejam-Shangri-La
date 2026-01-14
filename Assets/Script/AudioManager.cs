using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;
    public List<AudioClip> audioClips;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayPlaceSound()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    public void PlayFlipSound()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }
    
    public void PlayPickUpSound()
    {
        audioSource.clip = audioClips[2];
        audioSource.Play();
    }
    
    public void PlayBoardDoneSound()
    {
        audioSource.clip = audioClips[3];
        audioSource.Play();
    }
}