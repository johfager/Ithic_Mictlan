using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ActiveItem[] ActiveItems;
    public ConsumableItem[] ConsumableItems;
    public BuffConsumable[] BuffConsumables;
    public JaguarWarriorItem[] JaguarWarriorItems;
    public EagleWarriorItem[] EagleWarriorItems;
    public ChamanItem[] ChamanItems;
    public OwlWitchItem[] OwlWitchItems;

    void Awake(){
        ConsumableItems = new ConsumableItem[]
        {
            new ConsumableItem
            {
                id = 101,
                name = "Tamal de dulce",
                rarity = 1,
                description = "Un tamal de dulce siquesi. Recupera 30 de salud",
                healthRestored = 30.0f,
                manaRestored = 0,
            },
            

        };

        ActiveItems = new ActiveItem[]
        {
            new ActiveItem{
                id = 201,
                name = "Orbe misterioso",
                rarity = 2,
                description = "Un orbe misterioso. Su efecto es aleatorio",  
            },
            

        };

        BuffConsumables = new BuffConsumable[]
        {
            new BuffConsumable {
                id = 301,
                name = "Makahuitl",
                rarity = 1,
                description = "Incrementa el daño base del usuario en 15%", 

                healthBuff = 0,
                defenseBuff = 0,

                fallSpeed = 0,
                movSpeedBuff = 0,
                jumpHeightBuff = 0,

                dmgBuff = 1.2f,
                atkSpeedBuff = 0,

                critChanceBuff = 0,
            },
            

        };

        JaguarWarriorItems = new JaguarWarriorItem[]
        {
            new JaguarWarriorItem{
                id = 401,
                name = "Tótem de obsidiana",
                rarity = 2,
                description = "Al caer, Maira erguira 3 pilares de obsidiana que se volveran muros por 3 segundos",  
            },
            
        };

        EagleWarriorItems = new EagleWarriorItem[]
        {
            new EagleWarriorItem{
                id = 501,
                name = "Lanza etérea",
                rarity = 2,
                description = "Un orbe misterioso. Su efecto es aleatorio",  
            },

        };

        ChamanItems = new ChamanItem[]
        {
            new ChamanItem{
                id = 601,
                name = "Báculo universal",
                rarity = 2,
                description = "Un orbe misterioso. Su efecto es aleatorio",  
            },

        };

        OwlWitchItems = new OwlWitchItem[]
        {
            new OwlWitchItem{
                id = 701,
                name = "Plumas de búho ancestral",
                rarity = 2,
                description = "La Belleza del depredador ralentiza el movimiento y reduce la defensa de los enemigos golpeados.",  
            },

        };
    }
}
