using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoroutines : MonoBehaviour
{
    public static ItemCoroutines Instance;

    public void Awake(){
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }

    public void CallDestroyHover(GameObject spawnedItem){
        StartCoroutine(DestroyHoverItemPrefab(spawnedItem));
    }

    public IEnumerator DestroyHoverItemPrefab(GameObject spawnedItem){
        yield return new WaitForSeconds(2.5f);
        GameObject.Destroy(spawnedItem);
    }
}
