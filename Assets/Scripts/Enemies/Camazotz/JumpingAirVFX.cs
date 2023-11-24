using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class JumpingAirVFX : MonoBehaviour
{


    [SerializeField] private GameObject particleVFXPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    void PlayJumpingAirVFX()
    {
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);

        GameObject landingVFX = Instantiate(particleVFXPrefab, transform.position, rotation);

        // Set the VFX to play
        ParticleSystem particleSystem = landingVFX.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {

            particleSystem.Play();
        }
        Destroy(landingVFX, particleSystem.main.duration);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
