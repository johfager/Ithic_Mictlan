using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.AI;

public class AhuizotlBehaviour : MonoBehaviour
{
    [SerializeField, Header("Ahuizotl Scriptable Object")] private EnemyStats enemyStats; // El scriptable object que contiene la info del Ahuizotl
    [SerializeField] private Transform target; // El objetivo actual del agente
    public float scanRadius = 5f; // Radio de escaneo alrededor del agente 
    public LayerMask targetLayer; // Capa de objetos objetivo
    private float moveSpeed; // Velocidad de movimiento del agente
    private NavMeshAgent agent;
    private static Transform sharedTarget; // Objetivo compartido entre todos los agentes
    private Vector3 spawnPoint; // Punto de origen del agente
    private float timeCounter; // Contador de tiempo para el movimiento circular

    public float circleRadius = 5f; // Radio del movimiento circular
    public float circleSpeed = 1f; // Velocidad del movimiento circular

    // Estadisticas base del Ahuizotl
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        moveSpeed = agent.speed;
        spawnPoint = transform.position;
    }

    void Update()
    {
        if (sharedTarget == null)
        {
            // Move in a circular pattern
            timeCounter += Time.deltaTime * circleSpeed;
            float x = Mathf.Cos(timeCounter) * circleRadius + spawnPoint.x;
            float z = Mathf.Sin(timeCounter) * circleRadius + spawnPoint.z;
            Vector3 newPosition = new Vector3(x, transform.position.y, z);
            agent.SetDestination(newPosition);
            // Si no hay un objetivo compartido, escanea la zona en busca de uno
            ScanForTarget();
        }
        else
        {
            // Usa el objetivo compartido
            target = sharedTarget;
            agent.SetDestination(target.position);
        }
    }

    void ScanForTarget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, scanRadius, targetLayer);

        if (targets.Length > 0)
        {
            // Encontrar el objetivo más cercano dentro del radio de escaneo
            Transform closestTarget = null;
            float closestDistance = float.MaxValue;

            foreach (Collider col in targets)
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);

                if (distance < closestDistance)
                {
                    closestTarget = col.transform;
                    closestDistance = distance;
                }
            }

            // Asignar el objetivo encontrado a TODOS los agentes
            sharedTarget = closestTarget;
        }
    }

    void OnDrawGizmos()
    {
        // Draw a wire sphere to visualize the scanRadius in the scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
}
