using System.Collections;
using UnityEngine;

namespace Heroes.Rosa
{
    //TODO: Add a VFX prefab for the transformation that syncs with the model switching.
    public class UltimateRosa : MonoBehaviour
    {
        public GameObject newModel;
        public GameObject oldModel;
        private Avatar newAvatar; 
        public float delay = 10f; // delay in seconds
        private Avatar oldAvatar;
        private Animator animator;
        [SerializeField] GameObject transformationVFXPrefab;

        private void Start()
        {
            animator = GetComponentInParent<Animator>();
            newAvatar = newModel.GetComponent<Animator>().avatar;
            oldAvatar = oldModel.GetComponent<Animator>().avatar;
        }

        public void ReplaceModel()
        {
            // Start the VFX coroutine
            StartCoroutine(PlayVFX());

            // Switch avatar before changing models
            SwitchAvatar(newAvatar);

            // Hide the old model and show the new model
            oldModel.SetActive(false);
            newModel.SetActive(true);

            // Delay before reverting to the old model
            StartCoroutine(RevertAfterDelay());
        }

        IEnumerator PlayVFX()
        {

            // Instantiate the VFX prefab
            GameObject transformationVFX = Instantiate(transformationVFXPrefab, transform.position, Quaternion.identity);

            // Set the VFX to play
            ParticleSystem particleSystem = transformationVFX.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Play();
            }
            yield return new WaitForSeconds(0.5f);
            // Wait for the VFX duration before destroying it
            yield return new WaitForSeconds(particleSystem.main.duration);

            // Destroy the VFX after it finishes playing
            Destroy(transformationVFX);
        }

        void SwitchAvatar(Avatar avatar)
        {
            if (animator != null && avatar != null)
            {
                animator.avatar = avatar;
            }
            else
            {
                Debug.LogError("Animator or newAvatar is null. Make sure to assign the Animator component and a new Avatar in the Unity Editor.");
            }
        }

        IEnumerator RevertAfterDelay()
        {
            yield return new WaitForSeconds(delay);

            StartCoroutine(PlayVFX());
            // Hide the new model and show the old model
            newModel.SetActive(false);
            oldModel.SetActive(true);

            // Switch back to the original avatar
            SwitchAvatar(oldAvatar);
        }
    }
}
