using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioPlayer
{
    /// <summary>
    /// Plays the audio.
    /// </summary>
    public void PlayAudio();

    /// <summary>
    /// Stops the audio.
    /// </summary>
    public void StopAudio();

    /// <summary>
    /// Pauses the audio.
    /// </summary>
    public void PauseAudio();

    /// <summary>
    /// Rewinds the audio.
    /// </summary>
    /// <param name="time">The time.</param>
    public void RewindAudio(float time);

    /// <summary>
    /// Sets the volume of the audio.
    /// </summary>
    /// <param name="volume">The volume.</param>
    public void SetVolume(float volume);
    
}
