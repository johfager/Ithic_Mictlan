using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISelectScreenManager : MonoBehaviour
{
    public static UISelectScreenManager instance;
    [SerializeField] private TMP_Text selectedCharacter;
    [SerializeField] private TMP_Text charcterDescription;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void Start() {
        selectedCharacter.text = "Selecciona a tu guerrero";
        charcterDescription.text = "...";
    }

    public void SetCharacterInfo(string name, string desc)
    {
        selectedCharacter.text = name;
        charcterDescription.text = desc;
    }
}
