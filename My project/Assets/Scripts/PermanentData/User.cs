using System;
using System.Collections.Generic;

[Serializable]
public class User
{
    // Public properties representing user attributes.
    public string userName; // User's name.
    public int enemiesKilled; // Number of enemies killed.
    public int moneySpent; // Amount of money spent.
    public int bulletsShot; // Number of bullets shot.
    public List<string> PlayerItems; // The items of the player.
    public string gameTime; // Property for tracking game time in seconds.

    // Constructor to create a User object with specified attributes.
    public User(string name, int enemies = 0, List<string> items = null, int money = 0, int shot = 0, string time = "")
    {
        userName = name; // Initialize user name.
        enemiesKilled = enemies; // Initialize enemies killed.
        moneySpent = money; // Initialize money spent.
        bulletsShot = shot; // Initialize bullets shot.
        PlayerItems = items ?? new List<string>(); // Initialize player's items, default to empty list.
        gameTime = time; // Initialize game time.
    }
}
