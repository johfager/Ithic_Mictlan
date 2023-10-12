using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent,
 RequireComponent(typeof(AudioManager))]
public class AudioUI : MonoBehaviour
{
    [HideInInspector]
    public AudioManager audioManager;
    [Space]
    public Slider masterSlider;
    public Slider walkSlider;
    public Slider uiSlider;
    public Slider uxSlider;
    public Slider gameLoopSlider;

    private void OnEnable()
    {
        audioManager = GetComponent<AudioManager>();
        masterSlider.onValueChanged.AddListener(audioManager.SetMasterVolume);
        walkSlider.onValueChanged.AddListener(audioManager.SetWalkVolume);
        uiSlider.onValueChanged.AddListener(audioManager.SetUIVolume);
        uxSlider.onValueChanged.AddListener(audioManager.SetUXVolume);
        gameLoopSlider.onValueChanged.AddListener(audioManager.SetGameLoopVolume);
    }

    private void OnDisable()
    {
        masterSlider.onValueChanged.RemoveListener(audioManager.SetMasterVolume);
        walkSlider.onValueChanged.RemoveListener(audioManager.SetWalkVolume);
        uiSlider.onValueChanged.RemoveListener(audioManager.SetUIVolume);
        uxSlider.onValueChanged.RemoveListener(audioManager.SetUXVolume);
        gameLoopSlider.onValueChanged.RemoveListener(audioManager.SetGameLoopVolume);
    }
}
