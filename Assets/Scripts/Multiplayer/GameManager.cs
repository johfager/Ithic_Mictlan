using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public GameObject pauseCanvas;
    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstallCoroutine());
        SetPaused();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            SetPaused();
        }
    }

    public void Quit()
    {
        PhotonNetwork.LeaveRoom();
    }

    private void SetPaused()
    {
        pauseCanvas.SetActive(paused);
        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = paused;
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }

    IEnumerator InstallCoroutine()
    {
        yield return new WaitForSeconds(3f);
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 2, 0), Quaternion.identity);
    }
}
