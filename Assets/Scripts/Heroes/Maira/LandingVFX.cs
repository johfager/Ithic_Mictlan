using UnityEngine;

namespace Heroes.Maira
{
    public class LandingVFX : MonoBehaviour
    {
        
        public GameObject landingVFXPrefab;

        public void PlayLandingVFX()
        {
            Quaternion rotation = Quaternion.Euler(-90, 0, 0);
            // Instantiate the VFX prefab
            GameObject landingVFX = Instantiate(landingVFXPrefab, transform.position + transform.forward*2 + -transform.up , rotation);

            // Set the VFX to play
            ParticleSystem particleSystem = landingVFX.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                
                particleSystem.Play();
            }

            // Destroy the VFX after it finishes playing
            Destroy(landingVFX, particleSystem.main.duration);
        }
    }
}