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
        [SerializeField] private ParticleSystem leftHandFireVFX;
        [SerializeField] private ParticleSystem rightHandFireVFX;

        
        public void PlayFireHandVFX()
        {
            leftHandFireVFX.Play();
            rightHandFireVFX.Play();
        }

        public void StopFireHandVFX()
        {
            leftHandFireVFX.Stop();
            rightHandFireVFX.Stop();
        }
        public void TriggerUltimate()
        {
            Quaternion rotation = Quaternion.Euler(-90, 0, 0);
            GameObject spawningVFX = Instantiate(spawningVFXPrefab,
                transform.position + transform.forward * 2 + -transform.up, rotation);
            // Set the VFX to play
            ParticleSystem particleSystem = spawningVFX.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {

                particleSystem.Play();
            }

            _wrestlingRingObject = Instantiate(wrestlingRingPrefab, transform.position + -Vector3.up * 2.7f,
                Quaternion.identity);
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
            yield return StartCoroutine(ScaleDownAndDestroy(_wrestlingRingObject, 0.2f));
        }


        private IEnumerator ScaleDownAndDestroy(GameObject target, float scaleDownDuration)
        {
            
            // Get all colliders of the object
            Collider[] colliders = target.GetComponents<Collider>();

            // Disable all colliders 
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }
            
            float timePassed = 0;
            Vector3 initialScale = target.transform.localScale;

            while (timePassed < scaleDownDuration)
            {
                timePassed += Time.deltaTime;
                float lerpValue = timePassed / scaleDownDuration;

                target.transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, lerpValue);

                yield return null;
            }

            Destroy(target);
        }
    }
}

