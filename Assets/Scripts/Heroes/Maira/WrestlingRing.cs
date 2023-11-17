using UnityEngine;

namespace Heroes.Maira
{
    public class WrestlingRing : MonoBehaviour
    {
        void OnTriggerEnter(Collider other) 
        {
            if(other.gameObject.CompareTag("Hero"))
            {
                Debug.Log("Player inside of WrestlingRing");
                // Add VFX to specific body parts with a particle system
                // Modify the scale of the player GameObject to increase their size
            }
        }
    }
}
