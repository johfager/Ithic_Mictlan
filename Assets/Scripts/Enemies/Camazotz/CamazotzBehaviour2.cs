using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CamazotzBehaviour2 : MonoBehaviour
{
    public enum State
    {
        InitialState, InitialSelectionOfPlayerToTargetState, FirstPhaseState, BasicAttackState,
        CloseRangeBasicAttackState, LargeRangeBasicAttackState, SoulEaterAttackState,
        UpsideDownWorldAttackState, InfernalScreechAttackState, ChangeOfPlayerToTargetState,
        SecondPhaseState, SoulDevourerAttackState, PhaseTransitionState, CamazotzDeathState
    }

    [Header("Camazotz States DEBUG ONLY, DO NOT TOUCH")]
    [SerializeField] private State currentState; //Current state of the Camazotz
    [SerializeField] private EnemyStats enemyStats; 
    private SphereCollider detectionRange; //Aggro range of the Camazotz
    [SerializeField] private GameObject objective;
    private NavMeshAgent agent;
    private GameObject[] playerList;

    private bool isAttacking;
    private float playerDistance;
    [SerializeField] private float currentCamazotzHealth;
    private float maxCamazotzHealth;

    void Start()
    {
        detectionRange = GetComponent<SphereCollider>();
        currentState = State.InitialState;
        isAttacking = false;
        detectionRange.radius = enemyStats.visionAttributes.visionRange;
        currentCamazotzHealth = enemyStats.healthAttributes.maxHealth;
        playerList = GameObject.FindGameObjectsWithTag("Hero");
        agent = GetComponent<NavMeshAgent>();
        CamazotzAgentSetter();
    }

    void CamazotzAgentSetter()
    {
        agent.speed = enemyStats.movementAttributes.movementSpeed;
    }

    void Update()
    {
        HandleInputDebug();

        switch (currentState)
        {
            case State.InitialState:
                HandleInitialState();
                break;

            case State.InitialSelectionOfPlayerToTargetState:
                HandleInitialSelectionOfPlayerToTargetState();
                break;

            case State.FirstPhaseState:
                HandleFirstPhaseState();
                break;

            case State.BasicAttackState:
                HandleBasicAttackState();
                break;

            case State.CloseRangeBasicAttackState:
                HandleCloseRangeBasicAttackState();
                break;

            case State.LargeRangeBasicAttackState:
                HandleLargeRangeBasicAttackState();
                break;

            case State.SoulEaterAttackState:
                HandleSoulEaterAttackState();
                break;

            case State.UpsideDownWorldAttackState:
                HandleUpsideDownWorldAttackState();
                break;

            case State.InfernalScreechAttackState:
                HandleInfernalScreechAttackState();
                break;

            case State.ChangeOfPlayerToTargetState:
                HandleChangeOfPlayerToTargetState();
                break;

            case State.SecondPhaseState:
                HandleSecondPhaseState();
                break;

            case State.SoulDevourerAttackState:
                HandleSoulDevourerAttackState();
                break;

            case State.PhaseTransitionState:
                HandlePhaseTransitionState();
                break;

            case State.CamazotzDeathState:
                HandleCamazotzDeathState();
                break;
        }
    }

    // Métodos de manejo de cada estado, refactoreados
    private void HandleInitialState()
    {
        GetComponent<MeshRenderer>().material.color = Color.gray;
        if (isAttacking)
        {
            ChangeState(State.InitialSelectionOfPlayerToTargetState);
        }
    }

    private void HandleInitialSelectionOfPlayerToTargetState()
    {
        Debug.Log("Selecting random player");
        int pick = Random.Range(0, playerList.Length);
        objective = playerList[pick];
        agent.SetDestination(objective.transform.position);
        Debug.Log("My objective is " + objective);
        ChangeState(State.FirstPhaseState);
    }

    private void HandleFirstPhaseState()
    {
        Debug.Log("First Phase");
        GetComponent<MeshRenderer>().material.color = Color.yellow;
        int attackIndex = Random.Range(0, 4);

        if (currentCamazotzHealth <= 0)
        {
            ChangeState(State.CamazotzDeathState);
        }
        else if (currentCamazotzHealth <= maxCamazotzHealth / 2)
        {
            ChangeState(State.SecondPhaseState);
        }
        else if (attackIndex == 0)
        {
            ChangeState(State.BasicAttackState);
        }
        else if (attackIndex == 1)
        {
            ChangeState(State.SoulEaterAttackState);
        }
        else if (attackIndex == 2)
        {
            ChangeState(State.UpsideDownWorldAttackState);
        }
        else if (attackIndex == 3)
        {
            ChangeState(State.InfernalScreechAttackState);
        }
    }

    private void HandleBasicAttackState()
    {
        Debug.Log("Basic attack");
        Debug.Log("Selecting basic attack");
        int attackIndex = Random.Range(0, 2);
        playerDistance = Vector3.Distance(transform.position, objective.transform.position);
        Debug.Log("Distance to objective is " + playerDistance);

        if (playerDistance <= 4)
        {
            agent.stoppingDistance = 4;
            ChangeState(State.CloseRangeBasicAttackState);
        }
        else
        {
            agent.stoppingDistance = playerDistance;
            ChangeState(State.LargeRangeBasicAttackState);
        }
    }

    private void HandleCloseRangeBasicAttackState()
    {
        Debug.Log("Close range basic attack");
        int attackIndex = Random.Range(0, 2);
        AttacksSoftReset();
        PhaseChecker(attackIndex);
    }

    private void HandleLargeRangeBasicAttackState()
    {
        Debug.Log("Large range basic attack");
        int attackIndex = Random.Range(0, 2);
        AttacksSoftReset();
        PhaseChecker(attackIndex);
    }

    private void HandleSoulEaterAttackState()
    {
        Debug.Log("Soul Eater");
        int attackIndex = Random.Range(0, 2);
        AttacksSoftReset();
        PhaseChecker(attackIndex);
    }

    private void HandleUpsideDownWorldAttackState()
    {
        Debug.Log("Upside down world");
        int attackIndex = Random.Range(0, 2);
        AttacksSoftReset();
        PhaseChecker(attackIndex);
    }

    private void HandleInfernalScreechAttackState()
    {
        Debug.Log("Infernal Screech");
        Debug.Log("Screeching");
        int attackIndex = Random.Range(0, 2);
        AttacksSoftReset();
        PhaseChecker(attackIndex);
    }

    private void HandleChangeOfPlayerToTargetState()
    {
        playerList = GameObject.FindGameObjectsWithTag("Hero");
        int pick = Random.Range(0, playerList.Length);
        objective = playerList[pick];
        Debug.Log("My objective is " + objective);
        ChangeState(State.PhaseTransitionState);
    }

    private void HandleSecondPhaseState()
    {
        Debug.Log("Second Phase");
        GetComponent<MeshRenderer>().material.color = Color.blue;
        int attackIndex = Random.Range(0, 5);

        if (currentCamazotzHealth <= 0)
        {
            ChangeState(State.CamazotzDeathState);
        }
        else if (attackIndex == 0)
        {
            ChangeState(State.BasicAttackState);
        }
        else if (attackIndex == 1)
        {
            ChangeState(State.SoulEaterAttackState);
        }
        else if (attackIndex == 2)
        {
            ChangeState(State.UpsideDownWorldAttackState);
        }
        else if (attackIndex == 3)
        {
            ChangeState(State.InfernalScreechAttackState);
        }
        else if (attackIndex == 4)
        {
            ChangeState(State.SoulDevourerAttackState);
        }
    }

    private void HandleSoulDevourerAttackState()
    {
        Debug.Log("Soul Devourer");
        int attackIndex = Random.Range(0, 2);
        AttacksSoftReset();
        PhaseChecker(attackIndex);
    }

    private void HandlePhaseTransitionState()
    {
        if (currentCamazotzHealth > maxCamazotzHealth / 2)
        {
            ChangeState(State.FirstPhaseState);
        }
        else if (currentCamazotzHealth <= maxCamazotzHealth / 2)
        {
            ChangeState(State.SecondPhaseState);
        }
        else if (currentCamazotzHealth <= 0)
        {
            ChangeState(State.CamazotzDeathState);
        }
    }

    private void HandleCamazotzDeathState()
    {
        Debug.Log("I am dead");
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void ChangeState(State newState)
    {
        currentState = newState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hero")
        {
            objective = other.gameObject;
            isAttacking = true;
        }
    }

    private void AttacksSoftReset()
    {
        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(2.0f);
        agent.stoppingDistance = 0;
    }

    private void PhaseChecker(int attackIndex)
    {
        if (currentCamazotzHealth <= 0 || attackIndex == 0)
        {
            ChangeState(State.FirstPhaseState);
        }
        else
        {
            ChangeState(State.PhaseTransitionState);
        }
    }

    private void HandleInputDebug()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Debug.Log("Change phase");
            currentCamazotzHealth = maxCamazotzHealth / 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Debug.Log("Death");
            currentCamazotzHealth = 0;
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}
