using System.Collections;
using UnityEngine;

namespace Heroes.Rosa
{
    public class UltimateRosa: MonoBehaviour
    {
        public GameObject newModel;
        public GameObject oldModel;
        private Avatar newAvatar; 
        public float delay = 5f; // delay in seconds
        private Avatar oldAvatar;
        private Animator animator;
        
        private void Start()
        {
            animator = GetComponentInParent<Animator>();
            newAvatar = newModel.GetComponent<Animator>().avatar;
            oldAvatar = oldModel.GetComponent<Animator>().avatar;
        }
        public void ReplaceModel()
        {
            SwitchAvatar(newAvatar);
            oldModel.SetActive(false);
            newModel.SetActive(true);
            StartCoroutine(RevertAfterDelay());
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
            newModel.SetActive(false);
            oldModel.SetActive(true);
            SwitchAvatar(oldAvatar);
        }
    }
}