using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    //public MairaCombat mairaCombatScript;
    public HeroesCombat heroesCombatScript;
    public HeroStats playerStats; // Reference to the scriptable object

    private bool isMoving = true;
    public GameObject heroModel;

    void Start()
    {
        HealthSystem healthSystem = GetComponent<HealthSystem>();
        healthSystem.Initialize(playerStats.healthAttributes.maxHealth);
        playerMovement = GetComponent<PlayerMovement>();
        //mairaCombatScript = GetComponent<MairaCombat>();
        heroesCombatScript = GetComponent<HeroesCombat>();
    }

    void Update()
    {
        // // Check if the player is in combat mode
        // if (mairaCombatScript.IsInCombatMode)
        // {
        //     isMoving = false; // Disable movement
        // }
        // else
        // {
        //     isMoving = true; // Enable movement
        // }

        // if (isMoving)
        // {
        //     playerMovement.HandleMovement();
        // }
        // Debug.Log("combat mode is: " + mairaCombatScript.IsInCombatMode.ToString());
        // mairaCombatScript.HandleAttackStateMachine();
        
        // Check if the player is in combat mode
        if (heroesCombatScript.IsInCombatMode)
        {
            isMoving = false; // Disable movement
        }
        else
        {
            isMoving = true; // Enable movement
        }

        if (isMoving)
        {
            playerMovement.HandleMovement();
        }
        Debug.Log("combat mode is: " + heroesCombatScript.IsInCombatMode.ToString());
        heroesCombatScript.HandleAttackStateMachine();

        
    }
}
