using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    // Warrior Shield. Defense buff
    // Cempasuchil flower. Healing item
    // Obsidian collar. Boosts crit chance
    // Jade orb. ???

    public static ItemManager Instance;
    
    public Item[] CommonItemPool;
    [SerializeField] public GameObject[] CommonItemPrefabs;
    [SerializeField] public Sprite[] CommonItemSprites;


    public Item[] RareItemPool;
    public JaguarWarriorItem[] JaguarWarriorItems = new JaguarWarriorItem[]
        {
            new JaguarWarriorItem{
                id = 401,
                name = "Tótem de obsidiana",
                rarity = 2,
                description = "Al caer, Maira erguira 3 pilares de obsidiana que se volveran muros por 3 segundos",  
            },
            
        };
    public EagleWarriorItem[] EagleWarriorItems = new EagleWarriorItem[]
        {
            new EagleWarriorItem{
                id = 501,
                name = "Lanza etérea",
                rarity = 2,
                description = "Un orbe misterioso. Su efecto es aleatorio",  
            },

        };
    public ChamanItem[] ChamanItems = new ChamanItem[]
        {
            new ChamanItem{
                id = 601,
                name = "Báculo universal",
                rarity = 2,
                description = "Un orbe misterioso. Su efecto es aleatorio",  
            },

        };
    public OwlWitchItem[] OwlWitchItems = new OwlWitchItem[]
        {
            new OwlWitchItem{
                id = 701,
                name = "Plumas de búho ancestral",
                rarity = 2,
                description = "La Belleza del depredador ralentiza el movimiento y reduce la defensa de los enemigos golpeados.",  
            },

        };

    void Awake(){
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 

        CommonItemPool = new Item[]
        {
            new ConsumableItem
            {
                id = 101,
                name = "Flor de Cempasuchil",
                rarity = 1,
                description = "Recupera 30 de salud",
                healthRestored = 30.0f,
                itemPrefab = CommonItemPrefabs[0],
                
            },

            new ConsumableItem
            {
                id = 102,
                name = "Tamal de dulce",
                rarity = 1,
                description = "Recupera 80 de salud",
                healthRestored = 30.0f,
                itemPrefab = CommonItemPrefabs[1],
                
            },

            // new BuffConsumable {
            
            //     id = 301,
            //     name = "Makahuitl",
            //     rarity = 1,
            //     description = "Incrementa el daño base del usuario en 15%", 

            //     buffType = 7,

            //     healthBuff = 1f,
            //     defenseBuff = 1f,

            //     fallSpeed = 1f,
            //     movSpeedBuff = 1f,
            //     jumpHeightBuff = 1f,

            //     dmgBuff = 1.15f,
            //     atkSpeedBuff = 1f,

            //     critChanceBuff = 1f,

            //     itemSprite = CommonItemSprites[2],
                
            // },

            // new BuffConsumable {
            //     id = 302,
            //     name = "Escudo de guerrero",
            //     rarity = 1,
            //     description = "Incrementa la defensa base del usuario en 15%", 

            //     buffType = 2,

            //     healthBuff = 1f,
            //     defenseBuff = 1.15f,

            //     fallSpeed = 1f,
            //     movSpeedBuff = 1f,
            //     jumpHeightBuff = 1f,

            //     dmgBuff = 1f,
            //     atkSpeedBuff = 1f,

            //     critChanceBuff = 1f,

            //     itemSprite = CommonItemSprites[3],
                
            // }, 

            // new BuffConsumable {
            //     id = 302,
            //     name = "Colgante de obsidiana",
            //     rarity = 1,
            //     description = "Incrementa la probabilidad de critco del usuario en 15%", 
            //     buffType = 8,

            //     healthBuff = 1f,
            //     defenseBuff = 1f,

            //     fallSpeed = 1f,
            //     movSpeedBuff = 1f,
            //     jumpHeightBuff = 1f,

            //     dmgBuff = 1f,
            //     atkSpeedBuff = 1f,

            //     critChanceBuff = 1.15f,

            //     itemSprite = CommonItemSprites[4],
                
            // }, 
            

        };

        RareItemPool = new Item[] {
            new ActiveItem{
                id = 201,
                name = "Orbe misterioso",
                rarity = 2,
                description = "Un orbe misterioso. Su efecto es aleatorio",  
            },
        };
        

    }

    public Item ChooseRandomCommonItem(){
        int randomIndex = Random.Range(0, CommonItemPool.Length);
        Item item = CommonItemPool[randomIndex];
        
        return item;
    }
}
