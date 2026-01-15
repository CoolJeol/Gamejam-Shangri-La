using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;
    public List<AudioResource> audioClips;

    public List<AudioClip> placedSound;
    public List<AudioClip> HmmSound;
    
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

    private void Update()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            PlayHmmSound();
        }
    }

    public void PlayPlaceSound()
    {
        audioSource.clip = placedSound[Random.Range(0, placedSound.Count)];
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

    public void PlayHmmSound()
    {
        var index = Random.Range(0, 100);
        if (index == 0)
        {
            audioSource.clip = HmmSound[^1];
            audioSource.Play();
        }
        else
        {
            audioSource.clip = HmmSound[Random.Range(0, HmmSound.Count - 1)];
            audioSource.Play();
        }
    }
}