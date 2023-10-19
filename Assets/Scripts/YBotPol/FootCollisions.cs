using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class FootCollisions : MonoBehaviour
{
    public static FootCollisions instance;

    //Type of ground
    public int type;
    //Delegate for foot collision events
    public delegate void FootCollisionEvent();
    public static event FootCollisionEvent OnFootContact;
    public delegate void FootCollisionRun();
    public static event FootCollisionRun OnFootContactRun;

    private void Awake()
    {
        //Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetGroundTag(int ground)
    {
        //Set the tag of the ground
        type = ground;
    }

    public int GetGroundTag()
    {
        //Return the tag of the ground
        return type;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //If the foot collides with the ground, invoke the event
        if (other.tag == "Sand" )  
        {
            OnFootContact();
            SetGroundTag(0);
        }
        else if (other.tag == "Sand" && Input.GetKey(KeyCode.LeftShift))
        {
            OnFootContactRun();
            SetGroundTag(0);
        }
        else if (other.tag == "Metal")
        {
            OnFootContact();
            SetGroundTag(1);
        }
        else if (other.tag == "Metal" && Input.GetKey(KeyCode.LeftShift))
        {
            OnFootContactRun();
            SetGroundTag(1);
        }
        else if (other.tag == "Grass")
        {
            OnFootContact();
            SetGroundTag(2);
        }
        else if (other.tag == "Grass" && Input.GetKey(KeyCode.LeftShift))
        {
            OnFootContactRun();
            SetGroundTag(2);
        }
        else if (other.tag == "Water")
        {
            OnFootContact();
            SetGroundTag(3);
        }
        else if (other.tag == "Water" && Input.GetKey(KeyCode.LeftShift))
        {
            OnFootContactRun();
            SetGroundTag(3);
        }
        
    }
}
