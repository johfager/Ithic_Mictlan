using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{

    public GameObject playerPrefab;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstallCoroutine());
    }
    IEnumerator InstallCoroutine()
    {
        yield return new WaitForSeconds(3f);
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 2, 0), Quaternion.identity);
    }
}
