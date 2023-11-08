using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class XoloController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animControl;
    private float speed;
    private bool isRunning;

    void Start() {
        speed = agent.speed;
    }

    void Update() {
        speed = agent.speed;
        if(speed == 10)
        {
            isRunning = true;
        }
        else if(speed == 3.5f)
        {
            isRunning = false;
        }

        animControl.SetBool("isRunning", isRunning);
        animControl.SetFloat("Speed", speed);
        
    }

}
