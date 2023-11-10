using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    [SerializeField] private bool playerInsideTrigger;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider trigger;

    void Start()
    {
        trigger = GetComponent<BoxCollider>();
        trigger.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInsideTrigger && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("Open", true);
            StartCoroutine(WaitAndDestroy());
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            playerInsideTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            playerInsideTrigger = false;
        }
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
