using System.Collections; // Required for Collections
using System.Collections.Generic; // Required for generic collections
using UnityEngine; // Required for Unity functionalities

// DungeonCrawler class represents a character that can move within a dungeon
public class DungeonCrawler : MonoBehaviour // Inherits from MonoBehaviour to integrate with Unity
{
    // Property to get or set the position of the DungeonCrawler in the dungeon
    public Vector2Int Position { get; set; } // Position of the DungeonCrawler represented as integer coordinates

    // Constructor to initialize the DungeonCrawler's starting position
    public DungeonCrawler(Vector2Int startPos) // Constructor takes the starting position as a parameter
    {
        Position = startPos; // Set the initial position
    }

    // Method to move the DungeonCrawler in a random direction
    public Vector2Int Move(Dictionary<Direction, Vector2Int> directionMovementMap) // Takes a map of directions and their corresponding movements
    {
        // Select a random direction from the provided directionMovementMap
        Direction toMove = (Direction)Random.Range(0, directionMovementMap.Count); // Get a random direction from the dictionary

        // Update the position based on the selected direction
        Position += directionMovementMap[toMove]; // Move the DungeonCrawler by updating its position

        // Return the updated position
        return Position; // Return the new position after the move
    }
}
