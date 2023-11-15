using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    [SerializeField, Tooltip("Assing here your scriptable object")]
    private HeroStats playerDataScriptableObject;
    
    public HeroStats GetInfo()
    {
        return playerDataScriptableObject;
    }

    public void SelectCharacter() {
        Debug.Log(playerDataScriptableObject.baseAttributes.characterName);
    } 
}