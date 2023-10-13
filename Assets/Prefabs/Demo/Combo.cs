using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Combo : MonoBehaviourPunCallbacks
{
    public Transform cTransform;
    public ParticleSystem ps; 
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(0))
            {
                photonView.RPC("RPC_Combat", RpcTarget.All);
            }
        }       
    }

    [PunRPC]
    void RPC_Combat()
    {
        ps.Play();
        Debug.Log("Golpe" + photonView.Owner.NickName);
    }
}
