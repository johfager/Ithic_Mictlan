using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public static SpawnPointManager instance;
    [SerializeField] public Transform[] spawns;
    private Transform playerSpawn;
    [SerializeField] private GameObject playerPrefab;

    void Awake() {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public Transform SetSpawnPoint(int HeroID)
    {
        if(HeroID == 1)
        {
            Debug.Log("Your character is Maira");
            playerSpawn = spawns[0];
        }
        if(HeroID == 2)
        {
            Debug.Log("Your character is Teo");
            playerSpawn = spawns[1];
        }
        if(HeroID == 3)
        {
            Debug.Log("Your character is Ignacio");
            playerSpawn = spawns[2];
        }
        if(HeroID == 4)
        {
            Debug.Log("Your character is Rosa");
            playerSpawn = spawns[3];
        }

        return playerSpawn;
    }

    public void SpawnPlayer(int ID)
    {
        playerSpawn = SetSpawnPoint(ID);
        Instantiate(playerPrefab, playerSpawn.position, Quaternion.Euler(playerSpawn.rotation.eulerAngles.x, playerSpawn.rotation.eulerAngles.y, playerSpawn.rotation.eulerAngles.z));
    }


}
