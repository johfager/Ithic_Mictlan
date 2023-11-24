using Heroes.Maira;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class ChanequeEnemy : MonoBehaviourPun
{
    private enum ChanequeState
    {
        Idle,
        Walk,
        Run,
        Attack
        // Add more states as needed (e.g., Attack)
    }
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private ChanequeState currentState;

    private HealthSystem _healthSystem;

    [SerializeField] private float runDistanceThreshold;
    [SerializeField] private float attackDistanceThreshold;

    public EnemyStats enemyStats;
    public Transform[] targets;

    private void Start()
    {
        
        ///TODO: Get list of players in scene from gamemanager here.
        /// 
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.InitializeHealth(enemyStats.healthAttributes.maxHealth);

        runDistanceThreshold = 40f;
        attackDistanceThreshold = 4f;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        ChangeState(ChanequeState.Idle);

        if (targets == null || targets.Length == 0)
        {
            Debug.LogError("Targets not assigned!");
        }

        // Call RPC to initialize the enemy state on all clients
        //photonView.RPC("InitializeEnemyState", RpcTarget.AllBuffered, enemyStats.healthAttributes.maxHealth);
    }

    private void Update()
    {
        // Check if this is the local player's enemy
        if (photonView.IsMine)
        {
            // Your AI logic to determine when to switch states
            Transform closestTarget = GetClosestTarget();

            if (closestTarget != null && Vector3.Distance(transform.position, closestTarget.position) < attackDistanceThreshold)
            {
                // Call RPC to sync the attack action across the network
                photonView.RPC("TriggerAttack", RpcTarget.AllBuffered);
            }
            else if (closestTarget != null)
            {
                navMeshAgent.SetDestination(closestTarget.position);
                ChangeState(ChanequeState.Walk);
            }
            else
            {
                ChangeState(ChanequeState.Idle);
            }
        }
    }

    private void ChangeState(ChanequeState newState)
    {
        // Handle state exit logic
        switch (currentState)
        {
            case ChanequeState.Idle:
                // Exit the Idle state
                // Debug.Log("Exiting Idle state");
                animator.SetBool("IsIdle", true);
                break;
            case ChanequeState.Walk:
                // Exit the Walk state
                // Debug.Log("Exiting Walk state");
                animator.SetBool("IsWalking", false);
                break;
            case ChanequeState.Run:
                // Exit the Run state
                // Debug.Log("Exiting Run state");
                animator.SetBool("IsRunning", false);
                break;
            case ChanequeState.Attack:
                // Exit the Attack state
                // Debug.Log("Exiting Attack state");
                animator.SetBool("IsAttacking", false);
                break;
            // Handle exit logic for other states as needed
        }

        // Handle state entry logic
        switch (newState)
        {
            case ChanequeState.Idle:
                // Enter the Idle state
                // Debug.Log("Entering Idle state");
                break;
            case ChanequeState.Walk:
                // Enter the Walk state
                // Debug.Log("Entering Walk state");
                animator.SetBool("IsWalking", true);
                break;
            case ChanequeState.Run:
                // Enter the Run state
                // Debug.Log("Entering Run state");
                animator.SetBool("IsRunning", true);
                break;
            case ChanequeState.Attack:
                // Enter the Attack state
                // Debug.Log("Entering Attack state");
                animator.SetBool("IsAttacking", true);
                break;
            // Handle entry logic for other states as needed
        }

        // Update the current state
        currentState = newState;
    }

    private void TriggerAttack()
    {
        Collider [] colliders = Physics.OverlapSphere(transform.position, 3f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Hero"))
            {
                collider.GetComponent<HealthSystem>().TakeDamage(enemyStats.combatAttributes.basicAttackDamage);
            }
        }
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

    public void ChangeTarget(Transform position)
    {
        // Set the new target
        targets[0] = position;
    }
}
