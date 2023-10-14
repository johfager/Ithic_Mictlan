using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Item
{
    // internal id of the item 
    int id{ get; set; }

    // name of the item which the player will see 
    string name{ get; set; }

    // 1 - common
    // 2 - rare
    // 3 - very rare
    // 4 - legendary
    int rarity{ get; set; }

    // short description of the item and its effect
    string description{ get; set; }

    void applyEffect();
}
