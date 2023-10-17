using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    [SerializeField] private Transform[] chestSpawnPoints;
    [SerializeField] private GameObject chest;
    [SerializeField] private int chestAmount;
    [SerializeField] private int time;
    [SerializeField] private float radioSpawn = 3f;

    // Start is called before the first frame update
    void Start()
    {
       foreach(Transform spawnPoint in chestSpawnPoints)
        {
            for (int i = 0; i < chestAmount; i++)
            {
                Vector3 randomPosition = Random.insideUnitSphere * radioSpawn;
                randomPosition.y = 0f;
                Instantiate(chest, spawnPoint.position + randomPosition, Quaternion.identity);
            }
        } 
    }

}
