<<<<<<< Updated upstream
using System.Collections;
using System.Collections.Generic;
=======
>>>>>>> Stashed changes
using UnityEngine;

public class Bullet : MonoBehaviour
{
<<<<<<< Updated upstream
    public float lifetime = 2f;

    void Start()
    {
=======
    public float lifetime = 2f; // Time before the bullet is destroyed

    void Start()
    {
        // Destroy the bullet after its lifetime
>>>>>>> Stashed changes
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
<<<<<<< Updated upstream
        // Add logic for what happens when the bullet hits something
        Destroy(gameObject);
    }
}
=======
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
>>>>>>> Stashed changes
