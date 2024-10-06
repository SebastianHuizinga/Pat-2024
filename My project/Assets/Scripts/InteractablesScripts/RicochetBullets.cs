using UnityEngine;

public class RicochetBullets : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player touches the item
        if (other.CompareTag("Player"))
        {
            UserSaveData saveData = other.GetComponent<UserSaveData>();
            if (saveData != null)
            {
                // Add the RicochetBullets item to the save system
                saveData.AddItem("RicochetBullets");

                // Save the game to ensure it's persisted
                saveData.SaveGame();
            }

            // Destroy the power-up after collection
            Destroy(gameObject); 
        }
    }
}
