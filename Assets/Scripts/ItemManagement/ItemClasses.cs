using System.Collections;
using UnityEngine;

// Regular consumer items, which restore health or mana to the player
// The values for health and mana are meant to be added to the player's total
// values can be 0
[System.Serializable]
public class ConsumableItem : Item {   
    // Inherit parameters from parent class
    public float healthRestored;

    // Model tied to the item (if applicable)
    public GameObject itemPrefab;

    public void applyEffect(HealthSystem playerHealth, Transform playerTransform){

        playerHealth.HealPlayer(healthRestored);
        GameObject prefabTemp = GameObject.Instantiate(itemPrefab, playerTransform.position + Vector3.up * 2.0f, playerTransform.rotation);
        prefabTemp.transform.parent = playerTransform;
        ItemCoroutines.Instance.CallDestroyHover(prefabTemp);
    }

}

// Items that buff player's base stats
// Values for each buff are meant to be multiplied by the player's stats. If there is no buff, the value should be 1, not 0
[System.Serializable]
public class BuffConsumable : Item{
    // 1 - health
    // 2 - defence
    // 3 - fallSpeed
    // 4 - movSpeed
    // 5 - jumpHeight
    // 6 - dmg
    // 7 - atkSpeed
    // 8 - critChance

    public int buffType;
    public float healthBuff;
    public float defenseBuff;

    public float fallSpeed;
    public float movSpeedBuff;
    public float jumpHeightBuff;

    public float dmgBuff;
    public float atkSpeedBuff;

    public float critChanceBuff;

    // Spirte tied to the item (if applicable)
    public Sprite itemSprite;

    

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


