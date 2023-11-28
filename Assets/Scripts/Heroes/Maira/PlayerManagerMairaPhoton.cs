using Photon.Pun;
using UnityEngine;

namespace Heroes.Maira
{
    public class PlayerManagerMairaPhoton : MonoBehaviour
    {
        public PlayerMovement playerMovement;
        public HeroesCombatMaira heroesCombatScript;
        public HeroStats heroStats;
        private bool isMoving;
        [SerializeField] private PhotonView photonView;
        [SerializeField] private GameObject canvas;

        void Start()
        {
            isMoving = true;
            HealthSystem healthSystem = GetComponent<HealthSystem>();
            healthSystem.InitializeHealth(heroStats.healthAttributes.maxHealth);
            playerMovement = GetComponent<PlayerMovement>();
            heroesCombatScript = GetComponent<HeroesCombatMaira>();
        }

        void Update()
        {
            if (photonView.IsMine)
            {
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
                playerMovement.HandleMovement();

                heroesCombatScript.HandleAttackStateMachine();
            }

        }
        public void ActivationUI()
        {
            if (photonView.IsMine)
            {
                canvas.SetActive(true);
            }
        }
    }
}
