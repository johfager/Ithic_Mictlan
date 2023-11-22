using System;
using System.Collections;
using UnityEngine;

namespace Heroes.Rosa
{
    public class Feather : MonoBehaviour
    {

        [SerializeField] private HeroStats rosaStats; // Set the damage amount

        public float maxRange = 5f;

        public Vector3 featherDirection = Vector3.forward;
        
        private Vector3 startPosition;
        private void OnEnable()
        {
            startPosition = transform.position;
        }
        

        void Update()
        {
            Launch(featherDirection);
            float distanceTravelled = Vector3.Distance(startPosition, transform.position);
    
            if (distanceTravelled > maxRange)
            {
                Destroy(gameObject);
            }
        }

        public void Launch(Vector3 direction)
        {
            GetComponent<Rigidbody>().AddForce(direction);
        }
        private void OnTriggerEnter(Collider other)
        {
            // Check if the collided object is an enemy
            if (other.gameObject.CompareTag("Enemy")) // assuming your enemies have an "Enemy" tag
            {
                // Apply damage to enemy
                other.gameObject.GetComponent<HealthSystem>().TakeDamage(rosaStats.abilityAttributes.primaryAbility.damage);

                // Destroy the feather game object
                Destroy(gameObject);
            }
        }
        
    }
}