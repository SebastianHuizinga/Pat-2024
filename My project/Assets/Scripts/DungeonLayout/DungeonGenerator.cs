using System.Collections; // Required for collections
using System.Collections.Generic; // Required for generic collections
using UnityEngine; // Required for Unity functionalities

public class DungeonGenerator : MonoBehaviour // Inherits from MonoBehaviour to integrate with Unity
{
    public DungeonGenerationData dungeonGenerationData; // Data object containing settings for dungeon generation
    private List<Vector2Int> dungeonRooms; // List to hold the locations of the generated dungeon rooms

    // List of possible room types that can be empty
    private List<string> emptyRooms = new List<string> { "enemy", "enemy2", "enemy3", "enemy4", "enemy5", "enemy6", "enemy7", "enemy8", "enemy9", "Shop" };

    private void Start() // Unity's Start method, called before the first frame update
    {
        // Generate dungeon rooms based on the provided generation data
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms); // Spawn the generated rooms in the dungeon
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms) // Method to spawn rooms based on their locations
    {
        RoomController.instance.LoadRoom("Start", 0, 0); // Load the starting room at the origin

        foreach (Vector2Int roomLocation in rooms) // Loop through each room location
        {
            string roomType; // Variable to hold the type of the current room

            // Check if the current room is the last room in the dungeon and is not the starting position
            if (roomLocation == dungeonRooms[dungeonRooms.Count - 1] && roomLocation != Vector2Int.zero)
            {
                roomType = "End"; // Set room type to "End" if it's the final room
            }
            else
            {
                // Check if there are no empty rooms left
                if (emptyRooms.Count == 0)
                {
                    // Array of possible enemy types
                    string[] enemyTypes = { "enemy", "enemy2", "enemy3", "enemy4", "enemy5", "enemy6", "enemy7", "enemy8", "enemy9" };

                    // Randomly select an enemy type
                    int randomIndex = Random.Range(0, enemyTypes.Length);
                    roomType = enemyTypes[randomIndex]; // Set room type to the randomly selected enemy type

                    // Output the selected roomType for debugging
                    Debug.Log("Room Type: " + roomType); // Log the type of room created for debugging
                }
                else
                {
                    roomType = emptyRooms.RandomItem(); // Select a random empty room type
                    emptyRooms.Remove(roomType); // Remove the selected room type from the list of empty rooms
                }
            }

            // Load the room at the specified location
            RoomController.instance.LoadRoom(roomType, roomLocation.x, roomLocation.y);
            // Debug.Log(roomType); // Uncomment to log the loaded room type
        }
    }
}

public static class ListExtensions // Static class to hold extension methods for lists
{
    // Extension method to select a random item from a list
    public static T RandomItem<T>(this List<T> list) // Generic method allowing selection from any list type
    {
        return list[Random.Range(0, list.Count)]; // Return a random item from the list
    }
}
