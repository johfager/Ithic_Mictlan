using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Clase encargada de controlar los canvas de la UI
/// </summary>
public class UIMenuController : MonoBehaviour
{
    [SerializeField] private CanvasGroup titleCanvasGroup;
    [Space]
    [SerializeField] private CanvasGroup menuButtonsCanvasGroup;
    [Space]
    [SerializeField] private CanvasGroup loadingScreenCanvasGroup;
    [SerializeField] private TMP_Text loadingScreenText;
    [Space]
    [SerializeField] private CanvasGroup createRoomCanvasGroup;
    [SerializeField] private TMP_InputField createRoomNameInputField;
    [Space]
    [SerializeField] private CanvasGroup joinedRoomCanvasGroup;
    [SerializeField] private TMP_Text joinedRoomNameText;
    [Space]
    [SerializeField] private CanvasGroup errorCanvasGroup;
    [SerializeField] private TMP_Text errorText;
    [Space]
    [SerializeField] private CanvasGroup roomBrowserCanvasGroup;
    [Space]
    [SerializeField] private CanvasGroup setNicknameCanvasGroup;
    [SerializeField] private TMP_InputField setNicknameInputField;
    [Space]
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private TMP_Text noRoomNameText;

    /// <summary>
    /// Inicializa los valores de todos los paneles
    /// </summary>
    public void InitGameScreen()
    {
        startGameButton.SetActive(false);

        titleCanvasGroup.alpha = 0f;
        titleCanvasGroup.interactable = false;
        titleCanvasGroup.blocksRaycasts = false;

        menuButtonsCanvasGroup.alpha = 0f;
        menuButtonsCanvasGroup.interactable = false;
        menuButtonsCanvasGroup.blocksRaycasts = false;

        loadingScreenCanvasGroup.alpha = 1f;
        loadingScreenCanvasGroup.interactable = true;
        loadingScreenCanvasGroup.blocksRaycasts = true;

        createRoomCanvasGroup.alpha = 0f;
        createRoomCanvasGroup.interactable = false;
        createRoomCanvasGroup.blocksRaycasts = false;

        joinedRoomCanvasGroup.alpha = 0f;
        joinedRoomCanvasGroup.interactable = false;
        joinedRoomCanvasGroup.blocksRaycasts = false;

        errorCanvasGroup.alpha = 0f;
        errorCanvasGroup.interactable = false;
        errorCanvasGroup.blocksRaycasts = false;

        roomBrowserCanvasGroup.alpha = 0f;
        roomBrowserCanvasGroup.interactable = false;
        roomBrowserCanvasGroup.blocksRaycasts = false;

        setNicknameCanvasGroup.alpha = 0f;
        setNicknameCanvasGroup.interactable = false;
        setNicknameCanvasGroup.blocksRaycasts = false;


        loadingScreenText.text = "Connecting...";

        ShowLoadingScreenCanvasGroupTween();
    }

    /// <summary>
    /// Sets the loading screen text
    /// </summary>
    /// <param name="text"></param>
    public void SetLoadingText(string text) => loadingScreenText.text = text;

    /// <summary>
    /// Gets the created room name
    /// </summary>
    /// <returns></returns>
    public string GetCreateRoomName() => createRoomNameInputField.text;

    /// <summary>
    /// Sets the joined room name
    /// </summary>
    /// <param name="roomName"></param>
    public void SetJoinedRoomName(string roomName) => joinedRoomNameText.text = "Room : " + roomName;

    /// <summary>
    /// Sets the error screen text
    /// </summary>
    /// <param name="errorName"></param>
    public void SetErrorText(string errorName) => errorText.text = errorName;

    /// <summary>
    /// Gets the player nickname
    /// </summary>
    /// <returns></returns>
    public string GetPlayerNickname() => setNicknameInputField.text;

    /// <summary>
    /// Gets the player nickname
    /// </summary>
    /// <returns></returns>
    public void SetPlayerNickname(string nickname) => setNicknameInputField.text = nickname;

    /// <summary>
    /// Activates the start game button
    /// </summary>
    /// <param name="status"></param>
    public void ActivateStartGameButton(bool status) => startGameButton.SetActive(status);

    private void ToggleCanvasGroupWithTween(CanvasGroup canvasGroup, float targetAlpha, bool interactable, bool blocksRaycasts){
        LeanTween.alphaCanvas(canvasGroup, targetAlpha, 0.5f).setOnComplete(() =>
        {
            canvasGroup.interactable = interactable;
            canvasGroup.blocksRaycasts = blocksRaycasts;
        });
    }

    /// <summary>
    /// Hides the title canvas group using tween
    /// </summary>
    public void HideTitleCanvasGroupTween()
    {
        ToggleCanvasGroupWithTween(titleCanvasGroup, 0f, false, false);
    }

    /// <summary>
    /// Shows the title canvas group using tween
    /// </summary>
    public void ShowTitleCanvasGroupTween()
    {
        ToggleCanvasGroupWithTween(titleCanvasGroup, 1f, true, true);
    }

    /// <summary>
    /// Hides the menu buttons canvas group using tween
    /// </summary>
    public void HideMenuButtonsCanvasGroupTween()
    {
        ToggleCanvasGroupWithTween(menuButtonsCanvasGroup, 0f, false, false);
    }

    /// <summary>
    /// Shows the menu buttons canvas group using tween
    /// </summary>
    public void ShowMenuButtonsCanvasGroupTween()
    {
        ToggleCanvasGroupWithTween(menuButtonsCanvasGroup, 1f, true, true);
    }

    /// <summary>
    /// Hides the loading screen canvas group using tween
    /// </summary>
    public void HideLoadingScreenCanvasGroupTween()
    {
        ToggleCanvasGroupWithTween(loadingScreenCanvasGroup, 0f, false, false);
    }

    /// <summary>
    /// Shows the loading screen canvas group using tween
    /// </summary>
    public void ShowLoadingScreenCanvasGroupTween()
    {
        ToggleCanvasGroupWithTween(loadingScreenCanvasGroup, 1f, true, true);
    }

    /// <summary>
    /// Hides the create room canvas group using tween
    /// </summary>
    public void HideCreateRoomCanvasGroupTween()
    {
        ToggleCanvasGroupWithTween(createRoomCanvasGroup, 0f, false, false);
    }

    /// <summary>
    /// Shows the create room canvas group using tween
    /// </summary>
    public void ShowCreateRoomCanvasGroupTween()
    {
        noRoomNameText.gameObject.SetActive(false);
        createRoomNameInputField.text = "";
        ToggleCanvasGroupWithTween(createRoomCanvasGroup, 1f, true, true);
    }

    /// <summary>
    /// Hides the joined room canvas group using tween
    /// </summary>
    public void HideJoinedRoomCanvasGroupTween()
    {
        ToggleCanvasGroupWithTween(joinedRoomCanvasGroup, 0f, false, false);
    }

    /// <summary>
    /// Shows the joined room canvas group using tween
    /// </summary>
    public void ShowJoinedRoomCanvasGroupTween()
    {
        ToggleCanvasGroupWithTween(joinedRoomCanvasGroup, 1f, true, true);
    }

    /// <summary>
    /// Hides the error canvas group using tween
    /// </summary>
    public void HideErrorCanvasGroupTween()
    {
        ToggleCanvasGroupWithTween(errorCanvasGroup, 0f, false, false);
    }

    /// <summary>
    /// Shows the error canvas group using tween
    /// </summary>
    public void ShowErrorCanvasGroupTween()
    {
        ToggleCanvasGroupWithTween(errorCanvasGroup, 1f, true, true);
    }

    /// <summary>
    /// Hides the room browser canvas group using tween
    /// </summary>
    public void HideRoomBrowserCanvasGroupTween()
    {
        ToggleCanvasGroupWithTween(roomBrowserCanvasGroup, 0f, false, false);
    }

    /// <summary>
    /// Shows the room browser canvas group using tween
    /// </summary>
    public void ShowRoomBrowserCanvasGroupTween()
    {
        ToggleCanvasGroupWithTween(roomBrowserCanvasGroup, 1f, true, true);
    }

    /// <summary>
    /// Hides the set nickname canvas group using tween
    /// </summary>
    public void HideSetNicknameCanvasGroupTween()
    {
        ToggleCanvasGroupWithTween(setNicknameCanvasGroup, 0f, false, false);
    }

    /// <summary>
    /// Shows the set nickname canvas group using tween
    /// </summary>
    public void ShowSetNicknameCanvasGroupTween()
    {
        //setNicknameInputField.text = "";
        ToggleCanvasGroupWithTween(setNicknameCanvasGroup, 1f, true, true);
    }

    public void ToggleNoRoomNameText(bool status) => noRoomNameText.gameObject.SetActive(status);

    // Colocar en todos los botones en el editor
    public void PreventDoubleClick()
    {
        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>(); 
        if (button != null)
        {
            button.interactable = false;
            // Reactivar boton despues de 1 segundo
            StartCoroutine(EnableButtonAfterDelay(button, 1f));
        }
    }

    private IEnumerator EnableButtonAfterDelay(Button button, float delay)
    {
        yield return new WaitForSeconds(delay);
        button.interactable = true;
    }
}
