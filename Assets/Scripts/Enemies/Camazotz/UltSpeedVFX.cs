using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltSpeedVFX : MonoBehaviour
{
    [SerializeField] private GameObject particleVFXPrefab;

    public void PlayUltSpeedVFX()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 0);

        GameObject landingVFX = Instantiate(particleVFXPrefab, particleVFXPrefab.transform.position, particleVFXPrefab.transform.rotation);

        // Set the VFX to play
        ParticleSystem particleSystem = landingVFX.GetComponent<ParticleSystem>();
        Debug.Log("particleSystem: " + particleSystem);
        if (particleSystem != null)
        {

            particleSystem.Play();
        }
        Destroy(landingVFX, particleSystem.main.duration);
    }
}
