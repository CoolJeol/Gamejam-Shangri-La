using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;
    public List<AudioResource> audioClips;
    
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
        audioSource.resource = audioClips[0];
        audioSource.Play();
    }

    public void PlayFlipSound()
    {
        audioSource.resource = audioClips[1];
        audioSource.Play();
    }
    
    public void PlayPickUpSound()
    {
        audioSource.resource = audioClips[2];
        audioSource.Play();
    }
    
    public void PlayBoardDoneSound()
    {
        audioSource.resource = audioClips[3];
        audioSource.Play();
    }
    
    public void PlaySlideBoardSound()
    {
        audioSource.resource = audioClips[4];
        audioSource.Play();
    }
}