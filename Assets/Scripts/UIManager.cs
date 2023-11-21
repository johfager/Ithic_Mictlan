// 2023-11-20 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using System;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterType 
{
    Hero,
    Enemy,
    Boss
}
//TODO: Handle Cooldowns inside this script as well?
public class UIManager : MonoBehaviour
{
    public Slider healthBar; // Reference to the health bar slider
    public event Action<float, CharacterType> OnHealthUpdated;

    private void Start()
    {
        // Subscribe to the OnHealthUpdated event
        OnHealthUpdated += HandleHealthUpdated;
    }
    
    public void SetMaxHealth(float maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }
    public void UpdateHealth(float health, CharacterType characterType)
    {
        // Invoke the OnHealthUpdated event
        OnHealthUpdated?.Invoke(health, characterType);
        
    }

    private void HandleHealthUpdated(float health, CharacterType characterType)
    {
        // Perform different actions depending on character type
        if(characterType == CharacterType.Hero)
        {
            // Update the health bar slider value
            healthBar.value = health; 
            // Player-specific UI update code
        } 
        else if(characterType == CharacterType.Enemy) 
        {
            Debug.Log("Enemy has taken damage, TODO: Update UI in UIManager");
            // Enemy-specific UI update code
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnHealthUpdated event
        OnHealthUpdated -= HandleHealthUpdated;
    }
}