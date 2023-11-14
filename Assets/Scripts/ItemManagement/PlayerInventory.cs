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

    private void Start(){
        playerHealth = GetComponent<HealthSystem>();
    }


    public void AddItem(Item item){
        // checks what type of item we got. if it's a buff, it's used instantly
        
        if (item is BuffConsumable buffItem) {
            Debug.Log($"Applied effect of buff {item.name}.");
            buffItem.applyEffect(heroStats);
            return;
        }
        // if it's a heal item, store it for later use
        if (Inventory.Count < 3){
            Inventory.Add(item);
            Debug.Log($"Added {item.name} to inventory.");
        } else {
            Debug.Log("Inventory is full.");
        }
        
    }

    public void RemoveItem(Item item) {
        Inventory.Remove(item);
        Debug.Log($"Removed {item.name} from inventory.");
    }

    public void UseItem(Item item) {
        if (item is ConsumableItem healItem) {
            healItem.applyEffect(playerHealth);
            RemoveItem(item);
        }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.F) && Inventory.Count != 0){
            UseItem(Inventory[0]);
        }
    }
}
