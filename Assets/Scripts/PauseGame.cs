using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Class in charge of pause the game
/// </summary>
[DisallowMultipleComponent]
public class PauseGame : MonoBehaviour
{
    public static Action<bool> OnGamePaused;
        
        private bool _isPaused;
        
        // Start is called before the first frame update
        private void Start()
        {
            _isPaused = false;
        }

        private void Update ()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _isPaused = !_isPaused;
                OnGamePaused?.Invoke(_isPaused);
            }
        }
}
