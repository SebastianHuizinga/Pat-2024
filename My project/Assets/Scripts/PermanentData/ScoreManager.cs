using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    
    public User user; // Current user based on input
    public TMP_InputField usernameInputField; 
    private string jsonFilePath;
    private string usernames;
    private string kills;
    private string shots;
    private string items;
    private string spendings;

    public TextMeshProUGUI userNameField;
    public TextMeshProUGUI enemiesKilledField;
    public TextMeshProUGUI moneySpentField;
    public TextMeshProUGUI bulletsShotField;

    private User[] userArray;

    // New variable to hold the current user
    private User currentUser;

    void Awake() // Use Awake for initialization before Start
    {
        jsonFilePath = Path.Combine(Application.persistentDataPath, "HighScores.json");
    }

    void Start()
    {
        // Load user scores from the JSON file.
        if (File.Exists(jsonFilePath)) // Check if the file exists before reading
        {
            string jsonContent = File.ReadAllText(jsonFilePath);
            List<User> userList = new List<User>(JsonHelper.FromJson<User>(jsonContent));
            userArray = userList.ToArray();

            // Optionally, load the user associated with the username input field
            string enteredUsername = usernameInputField.text;
            user = userList.FirstOrDefault(u => u.userName == enteredUsername); // Load the user by username

            // Sort the userArray by enemies killed in descending order.
            userArray = userArray.OrderByDescending(user => user.enemiesKilled).ToArray();
            userList = userArray.ToList();

            // Display the sorted user scores.
            displayScores(userArray);
        }
        else
        {
            Debug.LogWarning("HighScores.json file not found at: " + jsonFilePath);
        }
    }

    // Sort and display scores by money spent.
    public void orderBySpendings()
    {
        userArray = userArray.OrderByDescending(user => user.moneySpent).ToArray();
        displayScores(userArray);
    }

    // Sort and display scores by bullets shot.
    public void orderByShots()
    {
        userArray = userArray.OrderByDescending(user => user.bulletsShot).ToArray();
        displayScores(userArray);
    }

    // Sort and display scores by enemies killed.
    public void orderByKills()
    {
        userArray = userArray.OrderByDescending(user => user.enemiesKilled).ToArray();
        displayScores(userArray);
    }

    // Sort and display scores by username.
    public void orderByName()
    {
        userArray = userArray.OrderBy(user => user.userName).ToArray();
        displayScores(userArray);
    }

    // Display user scores in the UI.
    public void displayScores(User[] userArray)
    {
        // Clear existing score data.
        usernames = "";
        kills = "";
        shots = "";
        spendings = "";

        // Populate the UI text fields with user scores.
        for (int i = 0; i < userArray.Length; i++)
        {
            usernames += "#" + (i + 1) + " " + userArray[i].userName + "\n";
            userNameField.text = usernames;
        }
        for (int i = 0; i < userArray.Length; i++)
        {
            kills += userArray[i].enemiesKilled + "\n";
            enemiesKilledField.text = kills;
        }
        for (int i = 0; i < userArray.Length; i++)
        {
            shots += userArray[i].bulletsShot + "\n";
            bulletsShotField.text = shots;
        }
        for (int i = 0; i < userArray.Length; i++)
        {
            spendings += userArray[i].moneySpent + "\n";
            moneySpentField.text = spendings;
        }
    }

    public void SaveUsername()
    {
        string username = usernameInputField.text;

        // Load existing users
        List<User> users = FileHandler.ReadListFromJSON<User>("HighScores.json");

        // Create a new user if the username doesn't exist
        // Assuming the User constructor requires username, enemiesKilled, items, moneySpent, bulletsShot
        currentUser = users.FirstOrDefault(u => u.userName == username) ?? new User(username, 0, new List<string>(), 0, 0);
        
        // Add or update the user in the list
        if (!users.Contains(currentUser))
        {
            users.Add(currentUser); // Add the new user
        }

        // Save the updated list back to the file
        FileHandler.SaveToJSON(users, "HighScores.json");
        Debug.Log("Username saved: " + username);
    }

  public void IncrementBulletsShot()
{
    // Check if the currentUser is assigned
    if (currentUser != null)
    {
        // Increment the bullets shot count by 1
        currentUser.bulletsShot += 1;

        // Load existing users
        List<User> users = FileHandler.ReadListFromJSON<User>("HighScores.json");

        // Find the index of the currentUser in the users list
        int userIndex = users.FindIndex(u => u.userName == currentUser.userName);
        if (userIndex != -1)
        {
            // Update the user in the list
            users[userIndex] = currentUser;
        }

        // Save the updated list back to the file
        FileHandler.SaveToJSON(users, "HighScores.json");
        Debug.Log($"Incremented bullets shot for user: {currentUser.userName}. Total shots: {currentUser.bulletsShot}");
    }
    else
    {
        Debug.LogWarning("No current user assigned. Unable to increment bullets shot.");
    }
}


public void IncrementKills()
    {
        if (currentUser != null)
        {
            currentUser.enemiesKilled += 1;

            // Load existing users
            List<User> users = FileHandler.ReadListFromJSON<User>("HighScores.json");

            // Find the index of the currentUser in the users list
            int userIndex = users.FindIndex(u => u.userName == currentUser.userName);
            if (userIndex != -1)
            {
                // Update the user in the list
                users[userIndex] = currentUser;
            }

            // Save the updated list back to the file
            FileHandler.SaveToJSON(users, "HighScores.json");
            Debug.Log($"Incremented kills for user: {currentUser.userName}. Total kills: {currentUser.enemiesKilled}");
        }
        else
        {
            Debug.LogWarning("No current user assigned. Unable to increment kills.");
        }
    }

    // Method to update game time
    public void UpdateGameTime(string timeElapsed)
    {
        if (currentUser != null)
        {
            currentUser.gameTime += timeElapsed;

            // Load existing users
            List<User> users = FileHandler.ReadListFromJSON<User>("HighScores.json");

            // Find the index of the currentUser in the users list
            int userIndex = users.FindIndex(u => u.userName == currentUser.userName);
            if (userIndex != -1)
            {
                // Update the user in the list
                users[userIndex] = currentUser;
            }

            // Save the updated list back to the file
            FileHandler.SaveToJSON(users, "HighScores.json");
            Debug.Log($"Updated game time for user: {currentUser.userName}. Total game time: {currentUser.gameTime}");
        }
        else
        {
            Debug.LogWarning("No current user assigned. Unable to update game time.");
        }
    }
}
