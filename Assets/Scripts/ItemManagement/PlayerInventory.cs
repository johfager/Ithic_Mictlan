using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerInventory : MonoBehaviour
{   
    
    // Reference to the scriptable object containing the player's stats
    public HeroStats heroStats;
    // Inventory of the player the script is attached to
    [SerializeField] public List<Item> Inventory = new List<Item>();
    // Script containing information on the player's health
    public HealthSystem playerHealth;
    // A variable to store the player's current position
    private Transform playerPosition;
    
    // Set the playerHealth component to the player's HealthSystemScript
    private void Start(){
        playerHealth = GetComponent<HealthSystem>();
    }

    // Applies the effect of any buff the player picks up, or adds heal items to the inventory
    // Receives an Item type object. Returns nothing
    public void AddItem(Item item){
        // checks what type of item we got. if it's a buff, it's used instantly
        
        if (item is BuffConsumable buffItem) {
            
            buffItem.applyEffect(heroStats); // 
            return;
        }
        // if it's a heal item and the inventory is not full, store it for later use
        if (Inventory.Count < 3){
            Inventory.Add(item); // Add item to inventory array
        } // here is where an else statement would go to activate the canvas that notifies the player if their inventory is full
        
    }

    // Removes an item from the player's inventory
    // Receives an Item type object. Returns nothing
    public void RemoveItem(Item item) {
        Inventory.Remove(item);
    }

    // Uses a heal item from the player's inventory
    // Receives an Item type object. Returns nothing
    public void UseItem(Item item) {
        // Checks if the item is a consumable. Other items might use this method too eventually, so their respective checks can go here to allow them to have different logic
        if (item is ConsumableItem healItem) {
            playerPosition = transform;
            healItem.applyEffect(playerHealth, playerPosition); 
            RemoveItem(item);
        }
    }
    

    // Update checks for the KeyCode F and uses the first item on the queue
    void Update(){
        if(Input.GetKeyDown(KeyCode.F) && Inventory.Count != 0){
            UseItem(Inventory[0]);
        }
    }
}
