using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public List<AudioSource> audioSource;
    public List<AudioResource> audioClips;

    private int audioSourceIndex;
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
        audioSource[audioSourceIndex].clip = placedSound[Random.Range(0, placedSound.Count)];
        audioSource[audioSourceIndex].Play();
        audioSourceIndex = (int) Mathf.Repeat(audioSourceIndex++, audioClips.Count);
    }

    public void PlayFlipSound()
    {
        audioSource[audioSourceIndex].resource = audioClips[1];
        audioSource[audioSourceIndex].Play();
        audioSourceIndex = (int) Mathf.Repeat(audioSourceIndex++, audioClips.Count);
    }
    
    public void PlayPickUpSound()
    {
        audioSource[audioSourceIndex].resource = audioClips[2];
        audioSource[audioSourceIndex].Play();
        audioSourceIndex = (int) Mathf.Repeat(audioSourceIndex++, audioClips.Count);
    }
    
    public void PlayBoardDoneSound()
    {
        audioSource[audioSourceIndex].resource = audioClips[3];
        audioSource[audioSourceIndex].Play();
        audioSourceIndex = (int) Mathf.Repeat(audioSourceIndex++, audioClips.Count);
    }
    
    public void PlaySlideBoardSound()
    {
        audioSource[audioSourceIndex].resource = audioClips[4];
        audioSource[audioSourceIndex].Play();
        audioSourceIndex = (int) Mathf.Repeat(audioSourceIndex++, audioClips.Count);
    }

    public void PlayHmmSound()
    {
        var index = Random.Range(0, 100);
        if (index == 0)
        {
            audioSource[audioSourceIndex].clip = HmmSound[^1];
            audioSource[audioSourceIndex].Play();
            audioSourceIndex = (int) Mathf.Repeat(audioSourceIndex++, audioClips.Count);
        }
        else
        {
            audioSource[audioSourceIndex].clip = HmmSound[Random.Range(0, HmmSound.Count - 1)];
            audioSource[audioSourceIndex].Play();
            audioSourceIndex = (int) Mathf.Repeat(audioSourceIndex++, audioClips.Count);
        }
    }
}