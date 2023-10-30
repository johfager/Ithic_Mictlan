using UnityEngine;
using UnityEngine.AI;

public class ChanequeEnemy : EnemyManager
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private ChanequeState currentState;

    //TODO: This maybe should be inside Enemystats
    [SerializeField] private float runDistanceThreshold;

    //TODO: This is very quick and dirty.
    [SerializeField] private float attackDistanceThreshold;

    public EnemyStats enemyStats; // Set this in the Inspector with an EnemyStats asset
    public Transform[] targets; // Set this in the Inspector with an array of target Transforms

    private enum ChanequeState
    {
        Idle,
        Walk,
        Run,
        Attack
        // Add more states as needed (e.g., Attack)
    }

    private void Start()
    {
        runDistanceThreshold = 40f;
        attackDistanceThreshold = 4f;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Initialize the AI state to start with (e.g., Idle)
        ChangeState(ChanequeState.Idle);

        if (targets == null || targets.Length == 0)
        {
            Debug.LogError("Targets not assigned!");
        }
    }

    private void Update()
    {
        // Your AI logic to determine when to switch states
        Transform closestTarget = GetClosestTarget();

        // Check if the enemy is within attack range
        if (closestTarget != null && Vector3.Distance(transform.position, closestTarget.position) < attackDistanceThreshold)
        {
            ChangeState(ChanequeState.Attack);
        }
        // Otherwise, check if the enemy is within run range
        else if (closestTarget != null && Vector3.Distance(transform.position, closestTarget.position) < runDistanceThreshold)
        {
            ChangeState(ChanequeState.Run);
        }
        // Otherwise, walk to the closest target
        else if (closestTarget != null)
        {
            navMeshAgent.SetDestination(closestTarget.position);
            ChangeState(ChanequeState.Walk);
        }
        // Otherwise, idle
        else
        {
            ChangeState(ChanequeState.Idle);
        }
    }

    private void ChangeState(ChanequeState newState)
    {
        // Handle state exit logic
        switch (currentState)
        {
            case ChanequeState.Idle:
                // Exit the Idle state
                Debug.Log("Exiting Idle state");
                animator.SetBool("IsIdle", true);
                break;
            case ChanequeState.Walk:
                // Exit the Walk state
                Debug.Log("Exiting Walk state");
                animator.SetBool("IsWalking", false);
                break;
            case ChanequeState.Run:
                // Exit the Run state
                Debug.Log("Exiting Run state");
                animator.SetBool("IsRunning", false);
                break;
            case ChanequeState.Attack:
                // Exit the Attack state
                Debug.Log("Exiting Attack state");
                animator.SetBool("IsAttacking", false);
                break;
            // Handle exit logic for other states as needed
        }

        // Handle state entry logic
        switch (newState)
        {
            case ChanequeState.Idle:
                // Enter the Idle state
                Debug.Log("Entering Idle state");
                break;
            case ChanequeState.Walk:
                // Enter the Walk state
                Debug.Log("Entering Walk state");
                animator.SetBool("IsWalking", true);
                break;
            case ChanequeState.Run:
                // Enter the Run state
                Debug.Log("Entering Run state");
                animator.SetBool("IsRunning", true);
                break;
            case ChanequeState.Attack:
                // Enter the Attack state
                Debug.Log("Entering Attack state");
                animator.SetBool("IsAttacking", true);
                
                break;
            // Handle entry logic for other states as needed
        }

        // Update the current state
        currentState = newState;
    }

    Transform GetClosestTarget()
    {
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity; // TODO: Change this to a very large number
        foreach (Transform target in targets)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                closestTarget = target;
            }
        }
        return closestTarget;
    }
}
