using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Heroes.Maira
{
    
    public class UltimateVFX : MonoBehaviour
    {
        [SerializeField] private GameObject wrestlingRingPrefab;
        [SerializeField] GameObject spawningVFXPrefab;
        [SerializeField] private float duration = 5.0f;
        private GameObject _wrestlingRingObject;
        public void TriggerUltimate()
        {
            Quaternion rotation = Quaternion.Euler(-90, 0, 0);
            GameObject spawningVFX = Instantiate(spawningVFXPrefab, transform.position + transform.forward*2 + -transform.up , rotation);
            // Set the VFX to play
            ParticleSystem particleSystem = spawningVFX.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                
                particleSystem.Play();
            }
            _wrestlingRingObject = Instantiate(wrestlingRingPrefab, transform.position + -Vector3.up*2.7f, Quaternion.identity);
            // Destroy the VFX after it finishes playing
            Destroy(spawningVFX, particleSystem.main.duration);
            
            StartCoroutine(DestroyWrestlingRing());

        }


        private void OnApplicationQuit()
        {
            if (_wrestlingRingObject != null) Destroy(_wrestlingRingObject);
        }

        private IEnumerator DestroyWrestlingRing()
        {
            yield return new WaitForSeconds(duration);
            Destroy(_wrestlingRingObject);
        }
    }
}
