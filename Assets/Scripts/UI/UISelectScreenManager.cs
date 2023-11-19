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
    [SerializeField] private Button startGameButton;
    [SerializeField] private TMP_Text waitingText;
    [SerializeField] private PhotonMatchManager photonMatchManager;
    [SerializeField] private Button[] characterButtons;
    [SerializeField] private PhotonView photonView;
    private List<PhotonPlayerInfo> playerInfo;

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
        playerInfo = new List<PhotonPlayerInfo>();

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

    public void AddPlayerInfo(PhotonPlayerInfo data)
    {
        playerInfo.Add(data);
    }

    [PunRPC]
    public void HideSelectScreen()
    {
        characterSelectorPanel.alpha = 0f;
        characterSelectorPanel.interactable = false;
        characterSelectorPanel.blocksRaycasts = false;

        GameObject[] target = GameObject.FindGameObjectsWithTag("Hero");

        for (int i = 0; i < target.Length; i++)
        {
                    target[i].GetComponent<PlayerMovement>().enabled = true;
                    target[i].GetComponent<Heroes.PlayerManager>().enabled = true;
                    target[i].GetComponent<HeroesCombat>().enabled = true;
                    target[i].GetComponent<HealthSystem>().enabled = true;
        }    


        /*foreach (var player in playerInfo)
        {
            if(player.PlayerName == PhotonNetwork.NickName)
            {

                if(player.HeroID == 0)
                {
                    Debug.Log("For "+ PhotonNetwork.NickName + " I am enabling Maira");
                    GameObject target = GameObject.Find("Maira");
                    target.GetComponent<PlayerMovement>().enabled = true;
                    target.GetComponent<Heroes.PlayerManager>().enabled = true;
                    target.GetComponent<HeroesCombat>().enabled = true;
                    target.GetComponent<HealthSystem>().enabled = true;
                }
                else if(player.HeroID == 1)
                {
                    Debug.Log("For "+ PhotonNetwork.NickName + " I am enabling Teo");
                    GameObject target = GameObject.Find("Teo");
                    target.GetComponent<PlayerMovement>().enabled = true;
                    target.GetComponent<Heroes.PlayerManager>().enabled = true;
                    target.GetComponent<HeroesCombat>().enabled = true;
                    target.GetComponent<HealthSystem>().enabled = true;
                }
                else if(player.HeroID == 2)
                {
                    Debug.Log("For "+ PhotonNetwork.NickName + " I am enabling Ignacio");
                    GameObject target = GameObject.Find("Ignacio");
                    target.GetComponent<PlayerMovement>().enabled = true;
                    target.GetComponent<Heroes.PlayerManager>().enabled = true;
                    target.GetComponent<HeroesCombat>().enabled = true;
                    target.GetComponent<HealthSystem>().enabled = true;
                }
                else if(player.HeroID == 3)
                {
                    Debug.Log("For "+ PhotonNetwork.NickName + " I am enabling Rosa");
                    GameObject target = GameObject.Find("Rosa");
                    target.GetComponent<PlayerMovement>().enabled = true;
                    target.GetComponent<Heroes.PlayerManager>().enabled = true;
                    target.GetComponent<HeroesCombat>().enabled = true;
                    target.GetComponent<HealthSystem>().enabled = true;
                }
            }
        }*/

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
