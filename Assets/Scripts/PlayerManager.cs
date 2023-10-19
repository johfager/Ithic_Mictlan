using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public MairaCombat mairaCombatScript;
    private bool isMoving = true;
    public GameObject heroModel;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        mairaCombatScript = GetComponent<MairaCombat>();
    }

    void Update()
    {
        // Check if the player is in combat mode
        if (mairaCombatScript.IsInCombatMode)
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
        Debug.Log("combat mode is: " + mairaCombatScript.IsInCombatMode.ToString());
        mairaCombatScript.HandleAttackStateMachine();
    }
}
