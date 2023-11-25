using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBulletVFX : MonoBehaviour
{
    [SerializeField] private GameObject airBulletVFX;
    public float projectileSpeed = 10f;
    private float projectileDamage;

    public void LaunchAirBullet(Vector3 position, Vector3 objectivePos, Vector3 scale, float damage)
    {
        projectileDamage = damage;
        GameObject airBullet = Instantiate(airBulletVFX, position, transform.rotation);
        airBullet.transform.localScale = scale;

        airBullet.transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        HealthSystem objectiveLife = other.GetComponent<HealthSystem>();

        if (objectiveLife != null)
        {
            objectiveLife.TakeDamage(projectileDamage);
        }
    }
}
