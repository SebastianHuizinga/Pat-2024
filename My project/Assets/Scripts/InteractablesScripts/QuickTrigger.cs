using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTrigger : MonoBehaviour
{
    public float fireRateIncrease = 0.2f; // Amount to increase fire rate (as a percentage)

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that triggered the collider is the Player
        if (other.CompareTag("Player"))
        {
            PlayerShooting playerShooting = other.GetComponent<PlayerShooting>(); // Get the PlayerShooting component from the Player
            if (playerShooting != null) // Ensure the PlayerShooting component exists
            {
                // Access player's fire rate and increase it
                playerShooting.fireRate -= fireRateIncrease; // Decrease the time between shots by fireRateIncrease
                Debug.Log("QuickTrigger applied! Fire rate increased by " + fireRateIncrease * 100 + "%"); // Log the applied effect

                // Add item to player's save data
                UserSaveData saveData = other.GetComponent<UserSaveData>(); // Get the UserSaveData component to manage player's items
                if (saveData != null) // Ensure the UserSaveData component exists
                {
                    saveData.AddItem("QuickTrigger"); // Add the quick trigger item to player's inventory
                    Debug.Log("QuickTrigger added to inventory."); // Log the addition to inventory
                }
            }

            Destroy(gameObject); // Destroy the QuickTrigger object after it has been picked up
        }
    }
}
