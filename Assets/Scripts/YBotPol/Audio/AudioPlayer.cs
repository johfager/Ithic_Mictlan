using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour, IAudioPlayer
{
    public AudioClip audioClip;
    public AudioSource audioSource;

    public void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    public void PauseAudio()
    {
        audioSource.Pause();
    }

    public void PlayAudio()
    {
        audioSource.Play();
    }

    public void RewindAudio(float time)
    {
        audioSource.time = time;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
}
