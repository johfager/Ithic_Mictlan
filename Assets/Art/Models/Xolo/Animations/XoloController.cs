using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class XoloController : MonoBehaviour
{
    public static XoloController instance;
    private NavMeshAgent agent;
    private Animator animControl;
    private float speed;
    private bool isRunning;
    private bool wasCatched;
    private int numOfTouches;
    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        animControl = GetComponent<Animator>();
        speed = agent.speed;
        isRunning = false;
        wasCatched = false;
        numOfTouches = 0;
    }

    void Update() {
        speed = agent.speed;
        // Animation handling 
        if(speed == 10)
        {
            isRunning = true;
            numOfTouches = 1;
        }
        else if(speed == 3.5f)
        {
            isRunning = false;
            numOfTouches = 0;
        }

        // Setting parameters of the animator
        animControl.SetBool("isRunning", isRunning);
        animControl.SetFloat("Speed", speed);
        
    }

    public void StopRun()
    {
        Debug.Log("Stoping");
        wasCatched = true;
        animControl.SetBool("isRunning", false);
        animControl.SetFloat("Speed", 0);
        animControl.SetBool("hasStopped", wasCatched);
        
    }

    public bool CatchCheck()
    {
        return wasCatched;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Hero" && wasCatched == false)
        {
            numOfTouches = 2;
           StopRun();
            Debug.Log("You catched me");
        }

    }

}
