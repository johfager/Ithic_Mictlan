using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    [Header("Camazotz States DEBUG ONLY, DO NOT TOUCH")]
    [SerializeField] private State currentState; // Current state of Camazotz
    [SerializeField] private EnemyStats enemyStats; // Reference to the scriptable object
    private SphereCollider detectionRange; // Aggro range of Camazotz
    [SerializeField] private GameObject objective; // Objective that the enemy is currently targeting
    private NavMeshAgent agent; // NavMeshAgent component
    GameObject[] playerList;

    private bool isAtacking; // Aggro flag
    private float playerDistance; // Distance between player and enemy
    //Following variables is public to be able to test in game.
    [SerializeField] private float CurrentCamazotzhealth; 
    private float MaxCamazotzhealth; 
    // Start is called before the first frame update
    void Start()
    {
        detectionRange = GetComponent<SphereCollider>();
        currentState = State.InitialState;
        isAtacking = false;
        detectionRange.radius = enemyStats.visionAttributes.visionRange;
        CurrentCamazotzhealth = enemyStats.healthAttributes.maxHealth;
        playerList = GameObject.FindGameObjectsWithTag("Hero");
        agent = GetComponent<NavMeshAgent>();
        CamazotzAgentSetter();
    }

    void CamazotzAgentSetter()
    {
        agent.speed = enemyStats.movementAttributes.movementSpeed;
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
                int pick = Random.Range(0, playerList.Length);
                objective = playerList[pick];
                agent.SetDestination(objective.transform.position);
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
                if(playerDistance <= 4)
                {
                    agent.stoppingDistance = 4;
                    ChangeState(State.CloseRangeBasicAttackState);
                }
                else
                {
                    agent.stoppingDistance = playerDistance;
                    ChangeState(State.LargeRangeBasicAttackState);
                }

                break;

            case State.CloseRangeBasicAttackState:
                Debug.Log("Close range basic attack");
                attackIndex = Random.Range(0,2); // Determines if the attack hit or miss
                AttacksSoftReset();
                PhaseChecker(attackIndex);
                break;

            case State.LargeRangeBasicAttackState:
                Debug.Log("Large range basic attack");
                attackIndex = Random.Range(0,2); // Determines if the attack hit or miss
                AttacksSoftReset();
                PhaseChecker(attackIndex);
                break;

            case State.SoulEaterAttackState:
                Debug.Log("Soul Eater");
                attackIndex = Random.Range(0,2);
                AttacksSoftReset();
                PhaseChecker(attackIndex);
                break;

            case State.UpsideDownWorldAttackState:
                Debug.Log("Upside down world");
                attackIndex = Random.Range(0,2);
                AttacksSoftReset();
                PhaseChecker(attackIndex);
                break;

            case State.InfernalScreechAttackState:
                Debug.Log("Infernal Screech");
                Debug.Log("Screeching");
                attackIndex = Random.Range(0,2);
                AttacksSoftReset();
                PhaseChecker(attackIndex);
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
                AttacksSoftReset();
                PhaseChecker(attackIndex);
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

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Hero")
        {
            objective = other.gameObject;
            isAtacking = true;
        }
    }

    IEnumerator AttacksSoftReset()
    {
        yield return new WaitForSeconds(2.0f);
        agent.stoppingDistance = 0;
    }

    private void PhaseChecker(int attackIndex)
    {
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
    }
}
