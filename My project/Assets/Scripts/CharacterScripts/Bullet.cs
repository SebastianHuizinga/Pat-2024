using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2f; // Time before the bullet is destroyed

    void Start()
    {
        // Destroy the bullet after its lifetime
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the bullet on collision
        Destroy(gameObject);

        // Optional: Add logic to deal damage to the object it collides with
        // Example:
        // if (collision.gameObject.CompareTag("Enemy"))
        // {
        //     collision.gameObject.GetComponent<Enemy>().TakeDamage(damageAmount);
        // }
    }
}
