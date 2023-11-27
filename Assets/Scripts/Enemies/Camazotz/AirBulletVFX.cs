using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.SideChannels;
using UnityEngine;

public class AirBulletVFX : MonoBehaviour
{
    [SerializeField] private GameObject airBulletVFX;
    public float projectileSpeed = 75f;
    private float projectileDamage;

    public void LaunchAirBullet(Vector3 position, Vector3 scale, float damage)
    {
        projectileDamage = damage;
        GameObject airBullet = Instantiate(airBulletVFX, position, transform.rotation);
        airBullet.transform.localScale = scale;
        Rigidbody rb = airBullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * projectileSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        HealthSystem objectiveLife = other.GetComponent<HealthSystem>();

        if (objectiveLife != null)
        {
            objectiveLife.TakeDamage(projectileDamage);
        }
    }
}
