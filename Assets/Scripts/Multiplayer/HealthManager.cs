using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HealthManager : MonoBehaviourPunCallbacks, IPunObservable
{
    private Renderer[] visuals;
    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        visuals = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            StartCoroutine(Respawn());
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (int)stream.ReceiveNext();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    void SetRenderer(bool state)
    {
        foreach (var ren in visuals)
        {
            ren.enabled = state;
        }
    }

    IEnumerator Respawn()
    {
        SetRenderer(false);
        health = 100;
        GetComponent<CharacterController>().enabled = false;
        transform.position = new Vector3(0, 4, 0);
        yield return new WaitForSeconds(1);
        GetComponent<CharacterController>().enabled = true;
        SetRenderer(true);
    }
}
