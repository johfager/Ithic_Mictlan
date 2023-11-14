using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Regular consumer items, which restore health or mana to the player
// The values for health and mana are meant to be added to the player's total
// values can be 0
[System.Serializable]
public class ConsumableItem : Item {   
    // Inherit parameters from parent class
    public float healthRestored;

    public void applyEffect(HealthSystem playerHealth){
        playerHealth.HealPlayer(healthRestored);
    }

}

// Items that buff player's base stats
// Values for each buff are meant to be multiplied by the player's stats. If there is no buff, the value should be 1, not 0
[System.Serializable]
public class BuffConsumable : Item{

    public float healthBuff;
    public float fallSpeed;
    public float movSpeedBuff;
    public float jumpHeightBuff;

    public float dmgBuff;
    public float atkSpeedBuff;
    public float defenseBuff;

    public float critChanceBuff;

    public void applyEffect(HeroStats heroStats){
        
        // buffing health attributes
        heroStats.healthAttributes.maxHealth *= healthBuff;
        // buff movement attributes
        heroStats.movementAttributes.movementSpeed *= movSpeedBuff;
        heroStats.movementAttributes.gravitySpeed *= fallSpeed;
        heroStats.movementAttributes.jumpHeight *= jumpHeightBuff;
        // buff combat attributes
        heroStats.combatAttributes.basicAttackDamage *= dmgBuff;
        heroStats.combatAttributes.attackSpeed *= atkSpeedBuff;
        heroStats.combatAttributes.criticalHitChance *= critChanceBuff;
        heroStats.combatAttributes.defense *= defenseBuff;

    }
    
}

// ----------------------------------------------------------------
//----------------------------------------------------------------

public class JaguarWarriorItem : Item{   
    // Inherit parameters from parent class
    public void applyEffect(){
        Debug.Log("UsedJWItem");
    }

}

public class EagleWarriorItem : Item{   
    // Inherit parameters from parent class
    public void applyEffect(){
        Debug.Log("UsedEWItem");
    }

}

public class ChamanItem : Item{   
    // Inherit parameters from parent class
    public void applyEffect(){
        Debug.Log("UsedChamanItem");
    }
}

public class OwlWitchItem : Item{   
    // Inherit parameters from parent class

    public void applyEffect(){
        Debug.Log("UsedOWItem");
    }

}

public class ActiveItem : Item {

    public void applyEffect(){
        Debug.Log("Activated spacebar item");
    }
}