using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// THIS IS A BASE FOR THE BEHAVIOUR OF THE BOSS
// SOME THINGS WILL NEED SOME CHANGES

public class CamazotzBehaviour : MonoBehaviour
{
    public enum State // States of Camazotz
    {
        InitialState, InitialSelectionOfPlayerToTargetState, FirstPhaseState, BasicAttackState, CloseRangeBasicAttackState, 
        LargeRangeBasicAttackState, SoulEaterAttackState, 
        UpsideDownWorldAttackState, InfernalScreechAttackState, ChangeOfPlayerToTargetState, SecondPhaseState, 
        SoulDevourerAttackState, PhaseTransitionState, CamazotzDeathState
    }
    private State currentState; // Current state of Camazotz
    public EnemyStats enemyStats; // Reference to the scriptable object
    private SphereCollider detectionRange; // Aggro range of Camazotz
    private GameObject objective; // Objective that the enemy is currently targeting
    private bool isAtacking; // Aggro flag
    private float playerDistance; // Distance between player and enemy
    //Following variables is public to be able to test in game.
    public float CurrentCamazotzhealth; 
    private float MaxCamazotzhealth; 
    // Start is called before the first frame update
    void Start()
    {
        detectionRange = GetComponent<SphereCollider>();
        currentState = State.InitialState;
        isAtacking = false;
        detectionRange.radius = enemyStats.visionAttributes.visionRange;
        CurrentCamazotzhealth = enemyStats.healthAttributes.maxHealth;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            Debug.Log("Change phase");
            CurrentCamazotzhealth = MaxCamazotzhealth / 2;
        }
        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            Debug.Log("Death");
            CurrentCamazotzhealth = 0;
            GetComponent<MeshRenderer>().material.color = Color.red;
        }

        switch (currentState)
        {
            case State.InitialState:
                GetComponent<MeshRenderer>().material.color = Color.gray;
                // Idle animations 
                if(isAtacking)
                {
                    Debug.Log("Detection");
                    ChangeState(State.InitialSelectionOfPlayerToTargetState);
                }
                break;

            case State.InitialSelectionOfPlayerToTargetState:
                Debug.Log("Selecting random player");
                Debug.Log("Done");
                GameObject[] playerList = GameObject.FindGameObjectsWithTag("Hero");
                int pick = Random.Range(0, playerList.Length);
                objective = playerList[pick];
                Debug.Log("My objective is " + objective);
                ChangeState(State.FirstPhaseState);
                break;

            case State.FirstPhaseState:
                Debug.Log("First Phase");
                GetComponent<MeshRenderer>().material.color = Color.yellow;
                int attackIndex = Random.Range(0, 4);

                if(CurrentCamazotzhealth <= 0)
                {
                    ChangeState(State.CamazotzDeathState);
                }
                else if(CurrentCamazotzhealth <= MaxCamazotzhealth/2)
                {
                    ChangeState(State.SecondPhaseState);
                }
                else if(attackIndex == 0)
                {
                    ChangeState(State.BasicAttackState);
                }
                else if(attackIndex == 1)
                {
                    ChangeState(State.SoulEaterAttackState);
                }
                else if(attackIndex == 2)
                {
                    ChangeState(State.UpsideDownWorldAttackState);
                }
                else if(attackIndex == 3)
                {
                    ChangeState(State.InfernalScreechAttackState);
                }
                
                break;

            case State.BasicAttackState:
                Debug.Log("Basic attack");
                Debug.Log("Selecting basic attack");
                attackIndex = Random.Range(0,2);
                playerDistance = Vector3.Distance(transform.position, objective.transform.position);
                Debug.Log("Distance to objective is " + playerDistance);
                // Just for debug porpuse
                if(playerDistance <= 15)
                {
                    ChangeState(State.CloseRangeBasicAttackState);
                }
                else if(playerDistance > 15)
                {
                    ChangeState(State.LargeRangeBasicAttackState);
                }

                break;

            case State.CloseRangeBasicAttackState:
                Debug.Log("Close range basic attack");
                attackIndex = Random.Range(0,2);
                if(CurrentCamazotzhealth <= 0)
                {
                    ChangeState(State.FirstPhaseState);
                }
                else if(attackIndex == 0 && CurrentCamazotzhealth > MaxCamazotzhealth/2)
                {
                    ChangeState(State.FirstPhaseState);
                }
                else if(attackIndex == 0 && CurrentCamazotzhealth <= MaxCamazotzhealth/2)
                {
                    ChangeState(State.FirstPhaseState);
                }
                else if(attackIndex == 1)
                {
                    ChangeState(State.PhaseTransitionState);
                }
                break;

            case State.LargeRangeBasicAttackState:
                Debug.Log("Large range basic attack");
                attackIndex = Random.Range(0,2);
                if(CurrentCamazotzhealth <= 0)
                {
                    ChangeState(State.FirstPhaseState);
                }
                else if(attackIndex == 0 && CurrentCamazotzhealth > MaxCamazotzhealth/2)
                {
                    ChangeState(State.FirstPhaseState);
                }
                else if(attackIndex == 0 && CurrentCamazotzhealth <= MaxCamazotzhealth/2)
                {
                    ChangeState(State.FirstPhaseState);
                }
                else if(attackIndex == 1)
                {
                    ChangeState(State.PhaseTransitionState);
                }
                break;

            case State.SoulEaterAttackState:
                Debug.Log("Soul Eater");
                attackIndex = Random.Range(0,2);
                if(CurrentCamazotzhealth <= 0)
                {
                    ChangeState(State.FirstPhaseState);
                }
                else if(attackIndex == 0 && CurrentCamazotzhealth > MaxCamazotzhealth/2)
                {
                    ChangeState(State.FirstPhaseState);
                }
                else if(attackIndex == 0 && CurrentCamazotzhealth <= MaxCamazotzhealth/2)
                {
                    ChangeState(State.FirstPhaseState);
                }
                else if(attackIndex == 1)
                {
                    ChangeState(State.PhaseTransitionState);
                }
                break;

            case State.UpsideDownWorldAttackState:
                Debug.Log("Upside down world");
                attackIndex = Random.Range(0,2);
                if(CurrentCamazotzhealth <= 0)
                {
                    ChangeState(State.FirstPhaseState);
                }
                else if(attackIndex == 0 && CurrentCamazotzhealth > MaxCamazotzhealth/2)
                {
                    ChangeState(State.FirstPhaseState);
                }
                else if(attackIndex == 0 && CurrentCamazotzhealth <= MaxCamazotzhealth/2)
                {
                    ChangeState(State.FirstPhaseState);
                }
                else if(attackIndex == 1)
                {
                    ChangeState(State.PhaseTransitionState);
                }
                break;

            case State.InfernalScreechAttackState:
                Debug.Log("Infernal Screech");
                Debug.Log("Screeching");
                attackIndex = Random.Range(0,2);
                if(CurrentCamazotzhealth <= 0)
                {
                    ChangeState(State.FirstPhaseState);
                }
                else if(attackIndex == 0 && CurrentCamazotzhealth > MaxCamazotzhealth/2)
                {
                    ChangeState(State.FirstPhaseState);
                }
                else if(attackIndex == 0 && CurrentCamazotzhealth <= MaxCamazotzhealth/2)
                {
                    ChangeState(State.FirstPhaseState);
                }
                else if(attackIndex == 1)
                {
                    ChangeState(State.ChangeOfPlayerToTargetState);
                }
                break;

            case State.ChangeOfPlayerToTargetState:
                playerList = GameObject.FindGameObjectsWithTag("Hero");
                pick = Random.Range(0, playerList.Length);
                objective = playerList[pick];
                Debug.Log("My objective is " + objective);
                ChangeState(State.PhaseTransitionState);
                break;

            case State.SecondPhaseState:
                Debug.Log("Second Phase");
                GetComponent<MeshRenderer>().material.color = Color.blue;
                attackIndex = Random.Range(0, 5);

                if(CurrentCamazotzhealth <= 0)
                {
                    ChangeState(State.CamazotzDeathState);
                }
                else if(attackIndex == 0)
                {
                    ChangeState(State.BasicAttackState);
                }
                else if(attackIndex == 1)
                {
                    ChangeState(State.SoulEaterAttackState);
                }
                else if(attackIndex == 2)
                {
                    ChangeState(State.UpsideDownWorldAttackState);
                }
                else if(attackIndex == 3)
                {
                    ChangeState(State.InfernalScreechAttackState);
                }
                else if(attackIndex == 4)
                {
                    ChangeState(State.SoulDevourerAttackState);
                }
                break;

            case State.SoulDevourerAttackState:
                Debug.Log("Soul Devourer");
                attackIndex = Random.Range(0, 2);
                if(attackIndex == 0 && CurrentCamazotzhealth <= MaxCamazotzhealth/2)
                {
                    ChangeState(State.SecondPhaseState);
                }
                else if(CurrentCamazotzhealth <= 0)
                {
                    ChangeState(State.CamazotzDeathState);
                }
                else if(attackIndex == 1)
                {
                    ChangeState(State.PhaseTransitionState);
                }
                break;

            case State.PhaseTransitionState:
                if(CurrentCamazotzhealth > MaxCamazotzhealth/2)
                {
                    ChangeState(State.FirstPhaseState);
                }
                else if(CurrentCamazotzhealth <= MaxCamazotzhealth/2)
                {
                    ChangeState(State.SecondPhaseState);
                }
                else if(CurrentCamazotzhealth <= 0)
                {
                    ChangeState(State.CamazotzDeathState);
                }
                break;
                
            case State.CamazotzDeathState:
                Debug.Log("I am dead");
                GetComponent<MeshRenderer>().material.color = Color.red;
                break;
        }    
    }

    private void ChangeState(State newState)
    {
        currentState = newState;
    }

    /*
    // Update is called once per frame
    void Update()
    {
        CamazotzStates();
    }

    void CamazotzStates()
    {
        Idle();
    }

    void Idle()
    {
        //idle animation
        //Seek player while idle
        SeekPlayer();
    }

    void SeekPlayer()
    {
        //seek player animation
        //if player is in range, set objective to player and aggro
        if (isAtacking)
        {
            Debug.Log("My target is: " + objective.name);
            Aggro();
        }
    }

    void Aggro()
    {
        //aggro animation
        // Rand number to determine which attack to use except basic attack
        playerDistance = Vector3.Distance(transform.position, objective.transform.position);

        int attack = Random.Range(1, 5);
        if (attack == 1)
        {
            //if cooldown ready use attack
            SoulEater(playerDistance);
            // if not, use basic attack
            BasicAttack(playerDistance);
        }
        else if (attack == 2)
        {  
            // if cooldown ready use attack
            UpsideDownWorld();
            // if not, use basic attack
            //BasicAttack();
        }
        else if (attack == 3)
        {
            // if cooldown ready use attack
            Screech();
            // if not, use basic attack
            //BasicAttack();
        }
        else if (attack == 4)
        {
            //if second phase active and ultimate in cooldown, use ultimate
            Ultimate();
            // if not, use basic attack
            //BasicAttack();

        }
    }

    void BasicAttack(float distance)
    {
        //Calculate distance between player and enemy
        
        //if distance is less than 2, use basic attack 1
        if (distance < 2)
        {
            //basic attack 1
            Debug.Log("Basic Attack close range");
        }
        //if distance is more than 2, use basic attack 2
        else if (distance > 2)
        {
            //basic attack 2
            Debug.Log("Basic Attack large range");
        }
    }

    void SoulEater(float distance)
    {
        //Soul Eater attack
        if(distance <= 7)
        {
            //Soul Eater close range
        }
        else if(distance > 2)
        {
            //Soul Eater large range
        }

    }

    void UpsideDownWorld()
    {
        //Upside down world attack
    }

    void Screech()
    {
        //Screech attack, if it lands it changes objective
    }

    void SecondPhase()
    {
        //Second phase animation, triggered when health is below 50%
    }

    void Ultimate()
    {
        //Ultimate attack
    }

    void CheckHealth()
    {
        //if health is below 50%, trigger second phase
        SecondPhase();
    }

    void Death()
    {
        //death animation
    }*/

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Hero")
        {
            objective = other.gameObject;
            isAtacking = true;
        }
    }
}
