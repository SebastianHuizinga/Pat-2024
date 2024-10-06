using UnityEngine;
using System.Collections;

public class FlameBullets : MonoBehaviour
{
    // Other existing code...

    // Method called when another collider enters this object's trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger is the Player
        if (other.CompareTag("Player"))
        {
            // Get the PlayerShooting component from the Player
            PlayerShooting playerShooting = other.GetComponent<PlayerShooting>();
            if (playerShooting != null) // Ensure the component was found
            {
                // Enable the burning bullets effect for the player
                playerShooting.SetBurningBullet(true);
                Debug.Log("You have collected flame bullets!"); // Log the collection of flame bullets
            }

            // Destroy this power-up object after collection to prevent multiple pickups
            Destroy(gameObject);
        }
    }
}
