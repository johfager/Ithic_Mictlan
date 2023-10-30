using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

[DisallowMultipleComponent]
public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] private float _masterVolume;
    [SerializeField] private float _walkVolume;
    [SerializeField] private float _uiVolume;
    [SerializeField] private float _uxVolume;
    [SerializeField] private float _gameLoopVolume;

    private void OnEnable()
    {
        PauseGame.OnGamePaused += GamePaused;
    }

    private void OnDisable()
    {
        PauseGame.OnGamePaused -= GamePaused;
    }

    private void GamePaused(bool gamePaused)
    {
        if (gamePaused)
        {
            SetChanelVolume("Walk", -80);
            SetChanelVolume("UI", _uiVolume);
            SetChanelVolume("UX", -80);
            SetChanelVolume("GameLoop", -80);

        }
        else
        {
            SetChanelVolume("Walk", _walkVolume);
            SetChanelVolume("UI", -80);
            SetChanelVolume("UX", _uxVolume);
            SetChanelVolume("GameLoop", _gameLoopVolume);
        }
    }

    /// <summary>
    /// Sets the chanel volume without saving the value.
    /// </summary>
    /// <param name="chanelName">The name of the chanel.</param>
    /// <param name="volume">The volume to set.</param>
    private void SetChanelVolume(string chanelName, float volume)
    {
        audioMixer.SetFloat(chanelName, volume);
    }

    /// <summary>
    /// Sets the master volume.
    /// </summary>
    /// <param name="volume">The volume to set.</param>
    public void SetMasterVolume(float volume)
    {
        _masterVolume = volume;
        audioMixer.SetFloat("Master", volume);
    }

    /// <summary>
    /// Sets the walk volume.
    /// </summary>
    /// <param name="volume">The volume to set.</param>
    public void SetWalkVolume(float volume)
    {
        _walkVolume = volume;
        audioMixer.SetFloat("Walk", volume);
    }

    /// <summary>
    /// Sets the UI volume.
    /// </summary>
    /// <param name="volume">The volume to set.</param>
    public void SetUIVolume(float volume)
    {
        _uiVolume = volume;
        audioMixer.SetFloat("UI", volume);
    }

    /// <summary>
    /// Sets the UX volume.
    /// </summary>
    /// <param name="volume">The volume to set.</param>
    public void SetUXVolume(float volume)
    {
        _uxVolume = volume;
        audioMixer.SetFloat("UX", volume);
    }

    /// <summary>
    /// Sets the game loop volume.
    /// </summary>
    /// <param name="volume">The volume to set.</param>
    public void SetGameLoopVolume(float volume)
    {
        _gameLoopVolume = volume;
        audioMixer.SetFloat("GameLoop", volume);
    }

}
