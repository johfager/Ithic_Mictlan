using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f; // Default max health
    public float currentHealth;

    public void Initialize(float health)
    {
        maxHealth = health;
        currentHealth = maxHealth;
    }

    public void HealPlayer(float damage){
        currentHealth += damage;
        if (currentHealth > maxHealth){
            currentHealth = maxHealth;
            Debug.Log("Player's health was maxed out");
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Implement death logic here, such as destroying the GameObject
        // Destroy(gameObject);
        Debug.Log("You died!");
    }
}
