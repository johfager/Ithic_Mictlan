using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class XoloController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animControl;
    private float speed;
    private bool isRunning;
    private int numOfTouches;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        animControl = GetComponent<Animator>();
        speed = agent.speed;
    }

    void Update() {
        speed = agent.speed;
        // Animation handling 
        if(speed == 10)
        {
            isRunning = true;
        }
        else if(speed == 3.5f)
        {
            isRunning = false;
        }

        // Setting parameters of the animator
        animControl.SetBool("isRunning", isRunning);
        animControl.SetFloat("Speed", speed);
        
    }

}
