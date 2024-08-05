using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Bullet prefab to be instantiated
    public Transform handTransform; // Transform of the hand where the bullets will be spawned
    public float bulletSpeed = 20f; // Speed of the bullets
    public float fireRate = 0.5f; // Time between shots

    private float nextFireTime = 0f;

    void Update()
    {
        // Check if the player is trying to shoot
        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the bullet at the hand's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, handTransform.position, handTransform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Set the bullet's velocity
        rb.velocity = handTransform.up * bulletSpeed;
    }
}