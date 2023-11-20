using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f; // Default max health
    public float currentHealth;
    
    
    public void InitializeHealth(float health)
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
        Debug.Log($"{this.name} took {damage} damage");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    //Should signal to play death animation probably
    private void Die()
    {
        if (this.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
        Debug.Log("You died!");
    }
}
