using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSaveData : MonoBehaviour
{
    public UserData currentUser = new UserData();
    public Player player;

    private float elapsedTime;

    void Start()
    {
        // Initialize the player's items list
        currentUser.PlayerItems = new List<string>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Update player's position and health
        currentUser.PlayerPosition = transform.position;
        currentUser.PlayerHealth = player.GetCurrentHealth();
        
        // Format the game time in hours/minutes/seconds and store it
        currentUser.formattedGameTime = GetFormattedGameTime();
    }
public string GetFormattedTime()
{
    return currentUser.formattedGameTime; // Ensure this retrieves the formatted game time string
}

    public void AddItem(string itemName)
    {
        if (currentUser.PlayerItems == null)
        {
            currentUser.PlayerItems = new List<string>();
            Debug.Log("PlayerItems list was null, initialized a new list.");
        }

        if (!currentUser.PlayerItems.Contains(itemName))
        {
            currentUser.PlayerItems.Add(itemName);
            Debug.Log("Item added: " + itemName);

            // Apply item effect immediately after collecting
            ApplyItemEffect(itemName);
        }
        else
        {
            Debug.Log("Item already exists in the list: " + itemName);
        }
    }

    public void SaveGame()
    {
        Debug.Log("saved!");
        Debug.Log("Items to be saved: " + string.Join(", ", currentUser.PlayerItems));  // Log items before saving
        saveGameManager.currentSaveData.UserData = currentUser;
        saveGameManager.SaveGame();
    }

    public void LoadGame()
    {
        saveGameManager.LoadGame();
        currentUser = saveGameManager.currentSaveData.UserData;

        // Restore player's other states (items, position, health, etc.)
        transform.position = currentUser.PlayerPosition;
        player.SetCurrentHealth(currentUser.PlayerHealth);

        // Check and apply any saved abilities
        foreach (string item in currentUser.PlayerItems)
        {
            ApplyItemEffect(item);
        }
    }

    // Method to format the game time in hours/minutes/seconds
    public string GetFormattedGameTime()
    {
        int hours = Mathf.FloorToInt(elapsedTime / 3600);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    public void ApplyItemEffect(string itemName)
    {
        if (itemName == "SteelBullets")
        {
            // Apply the steel bullet effect
            Bullet bulletPrefab = player.GetComponent<PlayerShooting>().bulletPrefab.GetComponent<Bullet>();
            bulletPrefab.baseDamage += 10f;
            Debug.Log("Steel bullets reapplied! Damage increased.");
        }
        else if (itemName == "SporeBullets")
        {
            // Apply poison effect logic here
            Debug.Log("Poison effect applied from Spore Bullets!");
            // You can call a method to set poison effect in PlayerShooting or another relevant class
        }
        else if (itemName == "FlameBullets")
        {
            // Apply burn effect logic here
            Debug.Log("Burn effect applied from Flame Bullets!");
            // You can call a method to set burn effect in PlayerShooting or another relevant class
        }
        else if (itemName == "QuickTrigger")
        {
            // Apply quick trigger effect
            PlayerShooting playerShooting = player.GetComponent<PlayerShooting>();
            playerShooting.fireRate -= 0.2f; // Assuming fireRate is a float that represents time between shots
            Debug.Log("QuickTrigger reapplied! Fire rate increased.");
        }
        // Add other item effects here
    }
}

[System.Serializable]
public struct UserData
{
    public Vector3 PlayerPosition;
    public float PlayerHealth;
    public string formattedGameTime; // Store the formatted game time as a string
    public List<string> PlayerItems; // Store collected items as a list of strings (or IDs)
}
