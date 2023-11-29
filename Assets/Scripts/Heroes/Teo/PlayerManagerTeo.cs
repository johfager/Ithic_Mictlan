using UnityEngine;
using Photon.Pun;

namespace Heroes.Teo
{
    public class PlayerManagerTeo : MonoBehaviour
    {
        public PlayerMovement playerMovement;
        public HeroesCombatTeo heroesCombatScript;
        public HeroStats heroStats;
        private bool isMoving;
        [SerializeField] private PhotonView ph;
        [SerializeField] private GameObject canvas;

        void Start()
        {
            isMoving = true;
            HealthSystem healthSystem = GetComponent<HealthSystem>();
            healthSystem.InitializeHealth(heroStats.healthAttributes.maxHealth);
            playerMovement = GetComponent<PlayerMovement>();
            heroesCombatScript = GetComponent<HeroesCombatTeo>();
        }

        void Update()
        {
            if (ph.IsMine)
            {
                playerMovement.HandleMovement();

                heroesCombatScript.HandleAttackStateMachine();
            }
            // Check if the player is in combat mode
            /*if (heroesCombatScript.IsInCombatMode)
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
            }*/
            


        }

        public void ActivationUI()
        {
            if (ph.IsMine)
            {
                canvas.SetActive(true);
            }
        }
    }
}
