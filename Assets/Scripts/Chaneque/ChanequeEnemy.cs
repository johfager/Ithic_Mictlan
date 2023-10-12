using UnityEngine;
using UnityEngine.AI;

public class ChanequeEnemy : EnemyManager
{
    private NavMeshAgent navMeshAgent;
    public EnemyStats enemyStats; // Set this in the Inspector with an EnemyStats asset
    public Transform[] targets; // Set this in the Inspector with an array of target Transforms

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (targets == null || targets.Length == 0)
        {
            Debug.LogError("Targets not assigned!");
        }
    }

    void Update()
    {
        Transform closestTarget = GetClosestTarget();
        if (closestTarget != null)
        {
            navMeshAgent.SetDestination(closestTarget.position);
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
}
