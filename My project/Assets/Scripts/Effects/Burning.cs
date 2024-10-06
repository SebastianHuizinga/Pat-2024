using UnityEngine;

public class Burning : MonoBehaviour
{
    // Duration of the burning effect
    public float burnDuration = 5f; 

    // This method is called when another collider enters the trigger collider attached to this GameObject
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Get the PlayerShooting component from the player GameObject
            PlayerShooting playerShooting = other.GetComponent<PlayerShooting>();
            if (playerShooting != null)
            {
                // Access the bullet prefab from PlayerShooting and modify it to apply burn effect
                Bullet bulletPrefab = playerShooting.bulletPrefab.GetComponent<Bullet>();
                if (bulletPrefab != null)
                {
                    // Enable the burning effect on the bullets
                    bulletPrefab.isBurningBullet = true; 
                    // Set the duration for the burning effect
                    bulletPrefab.burnDuration = burnDuration; 
                    // Log a message to indicate that burning bullets are now being used
                    Debug.Log("Hey, you are now shooting Burning Bullets!");
                }
            }

            // Destroy the power-up object after the player collects it
            Destroy(gameObject);
        }
    }
}
