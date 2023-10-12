using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class TweenCanvasVisibility : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        CanvasVisibility(false, 0, 0);
        PauseGame.OnGamePaused += GamePaused;
    }

    private void OnDestroy()
    {
        PauseGame.OnGamePaused -= GamePaused;
    }

    private void GamePaused(bool isPaused)
    {
        var alpha = isPaused ? 1 : 0;
        CanvasVisibility(isPaused, alpha, 1f);
    }

    /// <summary>
    /// Sets the visibility of the canvas group
    /// </summary>
    public void CanvasVisibility(bool active, float alphaValue, float animTime)
    {
        _canvasGroup.interactable = active;
        LeanTween.alphaCanvas(_canvasGroup, alphaValue, animTime);
    }
}
