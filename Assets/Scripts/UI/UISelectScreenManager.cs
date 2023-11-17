using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class UISelectScreenManager : MonoBehaviour
{
    public static UISelectScreenManager instance;
    [SerializeField] private TMP_Text selectedCharacter;
    [SerializeField] private TMP_Text charcterDescription;
    [SerializeField] private CanvasGroup characterSelectorPanel;
    [SerializeField] private Button readyButton;
    [SerializeField] private TMP_Text waitingText;
    [SerializeField] private PhotonMatchManager photonMatchManager;
    private int HeroID;

    private bool isCharacterSelected;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void Start() {
        selectedCharacter.text = "Selecciona a tu guerrero";
        charcterDescription.text = "...";
        isCharacterSelected = false;
        readyButton.interactable = false;
        waitingText.gameObject.SetActive(false);
    }

    public void SetCharacterInfo(string name, string desc)
    {
        selectedCharacter.text = name;
        charcterDescription.text = desc;
        readyButton.interactable = true;

        if(name == "Maira")
        {
            HeroID = 1;
        }
        if(name == "Teo")
        {
            HeroID = 2;
        }
        if(name == "Ignacio")
        {
            HeroID = 3;
        }
        if(name == "Rosa")
        {
            HeroID = 4;
        }

    }

    public void SelectCharacter()
    {
        isCharacterSelected = true;
        readyButton.interactable = false;
        waitingText.gameObject.SetActive(true);

        photonMatchManager.ChangeStatSent(PhotonNetwork.LocalPlayer.ActorNumber, 0, HeroID);

        SpawnPointManager.instance.SpawnPlayer(HeroID, PhotonNetwork.NickName);

        HideSelectScreen();
    }

    public void HideSelectScreen()
    {
        characterSelectorPanel.alpha = 0f;
        characterSelectorPanel.interactable = false;
        characterSelectorPanel.blocksRaycasts = false;
    }
}
