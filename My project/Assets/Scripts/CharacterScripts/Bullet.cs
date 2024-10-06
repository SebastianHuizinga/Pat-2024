using UnityEngine; // Required for using Unity's core features
using System.Collections.Generic; // Ensure you have this for List<T>

public class Bullet : MonoBehaviour // Defines a class for bullet behavior
{
    public float baseDamage = 50f;           // Base damage dealt by this bullet
    public bool isBurningBullet = false;     // Flag to indicate if this bullet applies burn
    public bool isPoisoningBullet = false;   // Flag to indicate if this bullet applies poison
    public float burnDamage = 1f;            // Damage per second from burning
    public float burnDuration = 5f;          // Duration of the burn effect
    public float poisonDamage = 1f;          // Damage per second from poison
    public float poisonDuration = 5f;        // Duration of the poison effect
    public LayerMask enemyLayer;             // LayerMask to specify the enemy layer
    public LayerMask bulletLayer;            // LayerMask to specify the bullet layer
    public LayerMask wallLayer;              // LayerMask to specify the wall/object layer
    public LayerMask playerLayer;            // LayerMask to specify the player layer
    public GameObject impactEffect;          // Effect to instantiate on impact
    private ScoreManager scoreManager;       // Reference to the ScoreManager
    private int ricochetCount = 0;           // Number of ricochets remaining
    private bool ricochetEnabled = false;    // Flag to indicate if ricochet is enabled
    private Rigidbody2D rb;                  // Rigidbody2D reference
 
    private void Start() // Unity's method called when the script instance is being loaded
    {
        rb = GetComponent<Rigidbody2D>(); // Gets the Rigidbody2D component attached to the bullet

        // Set the bullet's velocity
        rb.velocity = transform.up * 10f; // Adjust speed as needed
    }

    public void SetScoreManager(ScoreManager manager) // Method to set the ScoreManager reference
    {
        scoreManager = manager; // Set the ScoreManager reference
    }

    void OnCollisionEnter2D(Collision2D collision) // Called when the bullet collides with another object
    {
        // Check if the collided object is not the player
        if (((1 << collision.gameObject.layer) & playerLayer) == 0) // If it's not the player layer
        {
            // Check if the collided object is an enemy
            if (((1 << collision.gameObject.layer) & enemyLayer) != 0) // If it is the enemy layer
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>(); // Get the Enemy component
                if (enemy != null) // Check if the enemy component exists
                {
                    enemy.TakeDamage(baseDamage); // Apply damage to the enemy
                    Debug.Log("Enemy took " + baseDamage + " damage from bullet."); // Log damage dealt

                    if (enemy.GetHealth() <= 0) // If the enemy's health is 0 or less
                    {
                        scoreManager?.IncrementKills(); // Use the scoreManager reference to increment kills
                        Debug.Log("Enemy has been defeated."); // Log enemy defeat
                    }

                    if (isBurningBullet) // If this is a burning bullet
                    {
                        enemy.ApplyBurn(burnDuration); // Apply burn effect to enemy
                    }

                    if (isPoisoningBullet) // If this is a poisoning bullet
                    {
                        enemy.ApplyPoison(poisonDuration); // Apply poison effect to enemy
                    }
                }
                else // If no Enemy component is found
                {
                    Debug.LogError("No Enemy component found on the collided object!"); // Log error
                }
            }

            // Destroy the bullet after dealing damage
            Destroy(gameObject); // Destroy the bullet object
        }
        // If it collides with the player, do nothing (the bullet will not be destroyed)
    }

    public void EnableRicochet(int count) // Method to enable ricochet behavior
    {
        ricochetCount = count; // Set the number of ricochets available
        ricochetEnabled = true; // Enable ricochet behavior
    }

    private void FindNextTarget(Collision2D collision) // Method to find the next target for ricochet
    {
        // Find all enemies in the vicinity (you can adjust the detection radius)
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 5f, enemyLayer); // Get nearby enemies

        // If there are enemies in range, pick one at random to ricochet to
        if (hits.Length > 0) // Check if any enemies were hit
        {
            // Exclude the enemy we've just hit
            List<Collider2D> potentialTargets = new List<Collider2D>(hits); // Create a list of potential targets
            potentialTargets.Remove(collision.collider); // Remove the current enemy from the list

            if (potentialTargets.Count > 0) // If there are still potential targets
            {
                // Pick a random enemy to ricochet to
                Collider2D target = potentialTargets[Random.Range(0, potentialTargets.Count)]; // Select a random target
                Enemy nextEnemy = target.GetComponent<Enemy>(); // Get the Enemy component from the target

                if (nextEnemy != null) // Check if the next enemy exists
                {
                    // Apply damage to the next enemy
                    nextEnemy.TakeDamage(baseDamage); // Damage the next enemy

                    // Ricochet logic
                    RicochetToNextTarget(collision); // Call the method to handle ricochet
                }
            }
        }
    }

    private void RicochetToNextTarget(Collision2D collision) // Method to handle the ricochet logic
    {
        Vector2 reflectDirection = Vector2.Reflect(transform.up, collision.contacts[0].normal); // Calculate reflection direction
        transform.up = reflectDirection; // Update bullet direction
        rb.velocity = reflectDirection * rb.velocity.magnitude; // Maintain the bullet's speed
    }
}
