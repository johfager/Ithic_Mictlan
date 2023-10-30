using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Items that restore health to players
public class ConsumableItem : Item{   
    // Inherit parameters from parent class
    public int id{ get; set; }
    public string name{ get; set; }
    public int rarity{ get; set; } 
    public string description{ get; set; }
    public float healthRestored{ get; set; }
    public float manaRestored{ get; set; }

    public override void applyEffect(){
        Debug.Log("UsedHealthItem");
    }

}

// Items that buff player's base stats
public class BuffConsumable : Item{
    public int id{ get; set; }
    public string name{ get; set; }
    public int rarity{ get; set; }
    public string description{ get; set; }

    public float healthBuff{ get; set; }
    public float defenseBuff{ get; set; }

    public float fallSpeed{ get; set; }
    public float movSpeedBuff{ get; set; }
    public float jumpHeightBuff{ get; set; }

    public float dmgBuff{ get; set; }
    public float atkSpeedBuff{ get; set; }

    public float critChanceBuff{ get; set; }

    public override void applyEffect(){
        Debug.Log("UsedBuffItem");
    }
    
}

public class JaguarWarriorItem : Item{   
    // Inherit parameters from parent class
    public int id{ get; set; }
    public string name{ get; set; }
    public int rarity{ get; set; }
    public string description{ get; set; }

    public override void applyEffect(){
        Debug.Log("UsedJWItem");
    }

}

public class EagleWarriorItem : Item{   
    // Inherit parameters from parent class
    public int id{ get; set; }
    public string name{ get; set; }
    public int rarity{ get; set; }
    public string description{ get; set; }

    public override void applyEffect(){
        Debug.Log("UsedEWItem");
    }

}

public class ChamanItem : Item{   
    // Inherit parameters from parent class
   public int id{ get; set; }
    public string name{ get; set; }
    public int rarity{ get; set; }
    public string description{ get; set; }

    public override void applyEffect(){
        Debug.Log("UsedChamanItem");
    }
}

public class OwlWitchItem : Item{   
    // Inherit parameters from parent class
    public int id{ get; set; }
    public string name{ get; set; }
    public int rarity{ get; set; }
    public string description{ get; set; }

    public override void applyEffect(){
        Debug.Log("UsedOWItem");
    }

}

public class ActiveItem : Item {
    public int id{ get; set; }
    public string name{ get; set; }
    public int rarity{ get; set; }
    public string description{ get; set; }

    public override void applyEffect(){
        Debug.Log("Activated spacebar item");
    }
}