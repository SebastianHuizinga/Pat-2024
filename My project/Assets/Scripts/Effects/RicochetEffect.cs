using UnityEngine;

public class RicochetEffect : MonoBehaviour
{
    // Maximum number of times the bullet can ricochet
    public int maxRicochets; 
    private int ricochetsRemaining; // Counter for remaining ricochets

    void Start()
    {
        // Initialize remaining ricochets to the maximum value
        ricochetsRemaining = maxRicochets; 
    }

    // This method is called when the bullet collides with another object
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if there are ricochets remaining
        if (ricochetsRemaining > 0)
        {
            // Logic to ricochet towards another target
            RicochetToNextTarget(collision);
            ricochetsRemaining--; // Decrease the remaining ricochets
        }
        else
        {
            // Destroy the bullet once all ricochets are used up
            Destroy(gameObject); 
        }
    }

    // Method to handle the ricochet logic
    void RicochetToNextTarget(Collision2D collision)
    {
        // Calculate the reflection direction based on the collision normal
        Vector2 reflectDirection = Vector2.Reflect(transform.up, collision.contacts[0].normal);
        transform.up = reflectDirection; // Update the bullet's direction

        // Get the Rigidbody2D component to maintain velocity
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Set the bullet's velocity to the reflected direction while maintaining speed
            rb.velocity = reflectDirection * rb.velocity.magnitude; 
        }

        // Find the next target enemy within a certain radius
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 5f, LayerMask.GetMask("Enemy")); // Replace "Enemy" with the actual layer name if different
        if (hits.Length > 0)
        {
            // Pick a random enemy to ricochet towards
            Collider2D nextTarget = hits[Random.Range(0, hits.Length)];
            Enemy enemy = nextTarget.GetComponent<Enemy>();

            if (enemy != null)
            {
                // Log the name of the enemy being targeted
                Debug.Log($"Ricocheting towards enemy: {enemy.name}"); 
            }
            else
            {
                // Log if no enemy component was found on the next target
                Debug.Log("No enemy component found on the next target."); 
            }
        }
        else
        {
            // Log if no enemies were found for the ricochet
            Debug.Log("No enemies found for ricochet."); 
        }
    }
}
