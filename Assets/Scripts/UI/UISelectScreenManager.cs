using System.Collections;
using System.Collections.Generic;
using Heroes.Maira;
using Heroes.Rosa;
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
    [SerializeField] private Button startGameButton;
    [SerializeField] private TMP_Text waitingText;
    [SerializeField] private PhotonMatchManager photonMatchManager;
    [SerializeField] private Button[] characterButtons;
    [SerializeField] private PhotonView photonView;
    [SerializeField] private Spawner xoloSpawner;

    private int HeroID;
    private int playersReady;
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
        playersReady = 0;
        selectedCharacter.text = "Selecciona a tu guerrero";
        charcterDescription.text = "...";
        isCharacterSelected = false;
        readyButton.interactable = false;
        waitingText.gameObject.SetActive(false);

        if(PhotonNetwork.IsMasterClient)
        {
            startGameButton.gameObject.SetActive(true);
        }
        else
        {
            startGameButton.gameObject.SetActive(false);
        }
    }

    public void SetCharacterInfo(string name, string desc)
    {
        if(isCharacterSelected)
        {
            return;
        }
        selectedCharacter.text = name;
        charcterDescription.text = desc;
        readyButton.interactable = true;

        if(name == "Maira")
        {
            HeroID = 0;
        }
        if(name == "Teo")
        {
            HeroID = 1;
        }
        if(name == "Ignacio")
        {
            HeroID = 2;
        }
        if(name == "Rosa")
        {
            HeroID = 3;
        }

    }

    public void StartMatch()
    {
        xoloSpawner.SetSpawns();
        photonView.RPC("HideSelectScreen", RpcTarget.All);
    }

    public void SelectCharacter()
    {
        isCharacterSelected = true;
        readyButton.interactable = false;
        waitingText.gameObject.SetActive(true);

        photonMatchManager.ChangeStatSent(PhotonNetwork.LocalPlayer.ActorNumber, 0, HeroID);
        photonView.RPC("DisableButton", RpcTarget.All, HeroID);

        SpawnPointManager.instance.SpawnPlayer(HeroID);
    }

    [PunRPC]
    public void HideSelectScreen()
    {
        characterSelectorPanel.alpha = 0f;
        characterSelectorPanel.interactable = false;
        characterSelectorPanel.blocksRaycasts = false;

        GameObject[] target = GameObject.FindGameObjectsWithTag("Hero");
        //TODO: make this better.
        for (int i = 0; i < target.Length; i++)
        {
            HeroesCombatRosa combatScriptRosa = target[i].GetComponent<HeroesCombatRosa>();
            PlayerManagerRosaPhoton managerScriptRosa = target[i].GetComponent<PlayerManagerRosaPhoton>();
            
            HeroesCombatMaira combatScriptMaira = target[i].GetComponent<HeroesCombatMaira>();
            PlayerManagerMairaPhoton managerScriptMaira = target[i].GetComponent<PlayerManagerMairaPhoton>();
            
            // For ROSA
            if(combatScriptRosa != null && managerScriptRosa != null)
            {
                target[i].GetComponent<PlayerManagerRosaPhoton>().enabled = true;
                target[i].GetComponent<HeroesCombatRosa>().enabled = true;
                target[i].GetComponent<PlayerMovement>().enabled = true;
                target[i].GetComponent<HealthSystem>().enabled = true;
            } 
            else if(combatScriptMaira != null && managerScriptMaira != null) 
            {
                target[i].GetComponent<PlayerManagerMairaPhoton>().enabled = true;
                target[i].GetComponent<HeroesCombatMaira>().enabled = true;
                target[i].GetComponent<PlayerMovement>().enabled = true;
                target[i].GetComponent<HealthSystem>().enabled = true;

            }
            else {
                target[i].GetComponent<Heroes.PlayerManager>().enabled = true;
                target[i].GetComponent<HeroesCombat>().enabled = true;
                target[i].GetComponent<PlayerMovement>().enabled = true;
                target[i].GetComponent<HealthSystem>().enabled = true;
            }

            /*if (photonView.IsMine)
            {
                target[i].GetComponentInChildren<Canvas>().enabled = true;
            }*/
            /*if (HeroID == 3)
            {
                target[i].GetComponent<PlayerManagerRosaPhoton>().enabled = true;
                target[i].GetComponent<HeroesCombatRosa>().enabled = true;
                target[i].GetComponent<PlayerMovement>().enabled = true;
                target[i].GetComponent<HealthSystem>().enabled = true;
            }
            else
            {
                target[i].GetComponent<Heroes.PlayerManager>().enabled = true;
                target[i].GetComponent<HeroesCombat>().enabled = true;
                target[i].GetComponent<PlayerMovement>().enabled = true;
                target[i].GetComponent<HealthSystem>().enabled = true;
            }*/

        }    

    }

    [PunRPC]
    public void DisableButton(int ID)
    {
        characterButtons[ID].interactable = false;
        playersReady++;
        if(PhotonNetwork.IsMasterClient)
        {
            if(playersReady == 1)
            {
                startGameButton.interactable = true;
            }
        }
    }
}
