using System.Collections;
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
    private bool isInSecondPhase; //Checks if the Camazotz is in the second phase
    [SerializeField] private EnemyStats enemyStats;
    private SphereCollider detectionRange; //Aggro range of the Camazotz
    [SerializeField] private GameObject objective;
    private NavMeshAgent agent;
    private GameObject[] playerList;

    private bool isAttacking;
    private float playerDistance;
    HealthSystem healthSystem;

    //Cooldowns for the attacks
    [SerializeField] private float basicAttackCooldown = 0.0f;
    [SerializeField] private float soulEaterCooldown = 0.0f;
    [SerializeField] private float upsideDownWorldCooldown = 0.0f;
    [SerializeField] private float infernalScreechCooldown = 0.0f;
    [SerializeField] private float soulDevourerCooldown = 0.0f;

    void Start()
    {
        detectionRange = GetComponent<SphereCollider>();
        currentState = State.InitialState;
        isAttacking = false;
        detectionRange.radius = enemyStats.visionAttributes.visionRange;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.Initialize(enemyStats.healthAttributes.maxHealth);
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

    // Q0
    private void HandleInitialState()
    {
        if (isAttacking)
        {
            ChangeState(State.InitialSelectionOfPlayerToTargetState);
        }
    }

    // Q1
    private void HandleInitialSelectionOfPlayerToTargetState()
    {
        int pick = Random.Range(0, playerList.Length);
        objective = playerList[pick];
        agent.SetDestination(objective.transform.position);
        ChangeState(State.FirstPhaseState);
    }

    // Q2
    private void HandleFirstPhaseState()
    {
        Debug.Log("First Phase"); // DONE
        int attackIndex = Random.Range(0, 4);

        if (healthSystem.currentHealth <= 0)
        {
            ChangeState(State.CamazotzDeathState); //Q13
        }
        else if (healthSystem.currentHealth <= healthSystem.maxHealth / 2)
        {
            ChangeState(State.SecondPhaseState); // Q10
        }
        else if (attackIndex == 0)
        {
            ChangeState(State.BasicAttackState); // Q3
        }
        else if (attackIndex == 1)
        {
            ChangeState(State.SoulEaterAttackState); // Q6
        }
        else if (attackIndex == 2)
        {
            ChangeState(State.UpsideDownWorldAttackState); // Q7
        }
        else if (attackIndex == 3)
        {
            ChangeState(State.InfernalScreechAttackState); // Q8
        }
    }

    // Q3
    private void HandleBasicAttackState()
    {
        if (basicAttackCooldown <= 0.0f)
        {
            playerDistance = Vector3.Distance(transform.position, objective.transform.position);
            if (playerDistance <= 15)
            {
                agent.stoppingDistance = 15;
                ChangeState(State.CloseRangeBasicAttackState); // Q4
            }
            else if (playerDistance <= 30)
            {
                agent.stoppingDistance = 30;
                ChangeState(State.LargeRangeBasicAttackState); // Q5
            }
            basicAttackCooldown = 3.0f; // Set a 5-second cooldown for basic attack
            StartCoroutine(ResetCooldown("BasicAttackCooldown"));
        }
        else
        {
            ChangeState(State.FirstPhaseState);
        }
    }

    // Q4
    private void HandleCloseRangeBasicAttackState()
    {
        int attackIndex = Random.Range(0, 2);
        AttacksSoftReset();
        PhaseChecker(attackIndex);
    }

    // Q5
    private void HandleLargeRangeBasicAttackState()
    {
        int attackIndex = Random.Range(0, 2);
        AttacksSoftReset();
        PhaseChecker(attackIndex);
    }

    // Q6
    private void HandleSoulEaterAttackState()
    {
        if (soulEaterCooldown <= 0.0f)
        {
            playerDistance = Vector3.Distance(transform.position, objective.transform.position);
            if (playerDistance <= 15)
            {
                agent.stoppingDistance = 15;
                int attackIndex = Random.Range(0, 2);
                AttacksSoftReset();
                PhaseChecker(attackIndex);
                soulEaterCooldown = 10.0f; // Set a 10-second cooldown for Soul Eater attack
                StartCoroutine(ResetCooldown("SoulEaterCooldown"));
            }
            else
            {
                ChangeState(State.FirstPhaseState);
            }
        }
        else
        {
            ChangeState(State.FirstPhaseState);
        }
    }

    // Q7
    private void HandleUpsideDownWorldAttackState()
    {
        if (upsideDownWorldCooldown <= 0.0f)
        {
            playerDistance = Vector3.Distance(transform.position, objective.transform.position);
            if (playerDistance <= 15)
            {
                agent.stoppingDistance = 15;
                int attackIndex = Random.Range(0, 2);
                AttacksSoftReset();
                PhaseChecker(attackIndex);
                upsideDownWorldCooldown = 12.0f; // Set a 12-second cooldown for Upside Down World attack
                StartCoroutine(ResetCooldown("UpsideDownWorldCooldown"));
            }
            else
            {
                ChangeState(State.FirstPhaseState);
            }
        }
        else
        {
            ChangeState(State.FirstPhaseState);
        }
    }

    // Q8
    private void HandleInfernalScreechAttackState()
    {
        if (infernalScreechCooldown <= 0.0f)
        {
            playerDistance = Vector3.Distance(transform.position, objective.transform.position);
            if (playerDistance <= 30)
            {
                agent.stoppingDistance = 30;
                int attackIndex = Random.Range(0, 2);
                AttacksSoftReset();
                // PhaseChecker(attackIndex);
                if (attackIndex == 0)
                {
                    ChangeState(State.FirstPhaseState);
                }
                else
                {
                    ChangeState(State.ChangeOfPlayerToTargetState);
                }
                infernalScreechCooldown = 15.0f; // Set a 15-second cooldown for Infernal Screech attack
                StartCoroutine(ResetCooldown("InfernalScreechCooldown"));
            }
            else
            {
                ChangeState(State.FirstPhaseState);
            }
        }
        else
        {
            ChangeState(State.FirstPhaseState);
        }
    }

    private void HandleChangeOfPlayerToTargetState()
    {
        playerList = GameObject.FindGameObjectsWithTag("Hero");
        int pick = Random.Range(0, playerList.Length);
        objective = playerList[pick];
        agent.SetDestination(objective.transform.position);
        ChangeState(State.PhaseTransitionState);
    }

    private void HandleSecondPhaseState()
    {
        Debug.Log("Second Phase");
        int attackIndex = Random.Range(0, 5);

        if (healthSystem.currentHealth <= 0)
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
        if (soulDevourerCooldown <= 0.0f)
        {
            playerDistance = Vector3.Distance(transform.position, objective.transform.position);
            if (playerDistance <= 15)
            {
                agent.stoppingDistance = 15;
                int attackIndex = Random.Range(0, 2);
                AttacksSoftReset();
                PhaseChecker(attackIndex);
                soulDevourerCooldown = 20.0f; // Set a 20-second cooldown for Soul Devourer attack
                StartCoroutine(ResetCooldown("SoulDevourerCooldown"));
            }
            else
            {
                ChangeState(State.SecondPhaseState);
            }
        }
        else
        {
            ChangeState(State.SecondPhaseState);
        }
    }

    private void HandlePhaseTransitionState()
    {
        Debug.Log("Phase Transition");
        if (healthSystem.currentHealth > healthSystem.maxHealth / 2)
        {
            ChangeState(State.FirstPhaseState);
        }
        else if (healthSystem.currentHealth <= healthSystem.maxHealth / 2)
        {
            isInSecondPhase = true;
            ChangeState(State.SecondPhaseState);
        }
        else if (healthSystem.currentHealth <= 0)
        {
            ChangeState(State.CamazotzDeathState);
        }
    }

    private void HandleCamazotzDeathState()
    {
        Debug.Log("Camazotz Death");
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
        agent.stoppingDistance = 15;
    }

    private void PhaseChecker(int attackIndex)
    {
        if (attackIndex == 0)
        {
            if (isInSecondPhase)
            {
                ChangeState(State.SecondPhaseState);
            }
            else
            {
                ChangeState(State.FirstPhaseState);
            }
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
            healthSystem.TakeDamage(3000);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Debug.Log("Death");
            healthSystem.TakeDamage(10000);
        }
    }

    private IEnumerator ResetCooldown(string cooldownType)
    {
        float cooldownTime = 0.0f;

        switch (cooldownType)
        {
            case "BasicAttackCooldown":
                cooldownTime = 5.0f;
                break;
            case "SoulEaterCooldown":
                cooldownTime = 10.0f;
                break;
            case "UpsideDownWorldCooldown":
                cooldownTime = 12.0f;
                break;
            case "InfernalScreechCooldown":
                cooldownTime = 15.0f;
                break;
            case "SoulDevourerCooldown":
                cooldownTime = 20.0f;
                break;
        }

        yield return new WaitForSeconds(cooldownTime);
        switch (cooldownType)
        {
            case "BasicAttackCooldown":
                basicAttackCooldown = 0.0f;
                break;
            case "SoulEaterCooldown":
                soulEaterCooldown = 0.0f;
                break;
            case "UpsideDownWorldCooldown":
                upsideDownWorldCooldown = 0.0f;
                break;
            case "InfernalScreechCooldown":
                infernalScreechCooldown = 0.0f;
                break;
            case "SoulDevourerCooldown":
                soulDevourerCooldown = 0.0f;
                break;
        }
    }
}
