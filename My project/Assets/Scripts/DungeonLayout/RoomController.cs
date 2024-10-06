using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Class representing information about a room in the dungeon
public class RoomInfo 
{
    public string name; // Name of the room
    public int X;       // X coordinate of the room
    public int Y;       // Y coordinate of the room
}

public class RoomController : MonoBehaviour
{
    private bool isPlayerMoving = false; // Flag to check if the player is currently moving
    private float doorCooldownTime = 0.5f; // Cooldown time for door transitions
    private float doorCooldownTimer = 0;    // Timer to track cooldown

    public GameObject player; // Reference to the player GameObject
    public static RoomController instance; // Singleton instance of RoomController

    string currentWorldName = ""; // Name of the current world
    RoomInfo currentLoadRoomData; // Data for the room currently being loaded
    Room currRoom; // Reference to the current room
    Door currDoor; // Reference to the current door

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>(); // Queue for rooms to be loaded
    public List<Room> loadedRooms = new List<Room>(); // List of loaded rooms

    bool isLoadingRoom = false; // Flag to check if a room is currently being loaded

    void Awake()
    {
        instance = this; // Set the singleton instance
    }

    void Start() 
    {
        // Initialization logic can be placed here if needed
    }

    void Update() 
    {
        UpdateRoomQueue(); // Update the room loading queue

        // Update door cooldown timer
        if (isPlayerMoving) 
        {
            doorCooldownTimer -= Time.deltaTime; // Reduce the timer
            if (doorCooldownTimer <= 0) 
            {
                isPlayerMoving = false; // Reset movement flag
            }
        }
    }

    void UpdateRoomQueue()
    {
        if (isLoadingRoom) // Check if a room is currently being loaded
        {
            return; // Exit if loading
        }
        if (loadRoomQueue.Count == 0) // Check if there are rooms to load
        {
            return; // Exit if no rooms to load
        }

        currentLoadRoomData = loadRoomQueue.Dequeue(); // Get the next room data
        isLoadingRoom = true; // Set loading flag

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData)); // Load the room
    }

    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(x, y)) // Check if the room already exists
        {
            return; // Exit if it does
        }
        
        RoomInfo newRoomData = new RoomInfo(); // Create new room data
        newRoomData.name = name; // Set room name
        newRoomData.X = x;       // Set room X coordinate
        newRoomData.Y = y;       // Set room Y coordinate

        loadRoomQueue.Enqueue(newRoomData); // Add new room data to the queue
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name; // Construct the full room name

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive); // Load the scene asynchronously

        while (loadRoom.isDone == false) // Wait until the scene is fully loaded
        {
            yield return null; // Wait for the next frame
        }
    }

    public void RegisterRoom(Room room)
    {
        if (!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y)) // Check if the room does not exist
        {
            // Set room position based on coordinates and dimensions
            room.transform.position = new Vector3(
                currentLoadRoomData.X * room.Width, 
                currentLoadRoomData.Y * room.Height, 
                0
            );

            room.X = currentLoadRoomData.X; // Set room's X coordinate
            room.Y = currentLoadRoomData.Y; // Set room's Y coordinate
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y; // Set room name
            room.transform.parent = transform; // Set room as a child of the RoomController

            isLoadingRoom = false; // Reset loading flag

            loadedRooms.Add(room); // Add room to the loaded rooms list
            room.RemoveUnconnectedDoors(); // Remove any doors that are not connected
        } 
        else 
        {
            Destroy(room.gameObject); // Destroy the room if it already exists
            isLoadingRoom = false; // Reset loading flag
        }
    }

    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null; // Check if a room exists at the given coordinates
    }

    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y); // Find and return the room at the given coordinates
    }

    public void OnPlayerEnterRoom(Room room) 
    {
        currRoom = room; // Set the current room to the one the player entered

        // Only allow movement if not in cooldown
        if (!isPlayerMoving) 
        {
            // Move the player based on the current door's position and type
            if ((currDoor.getDoorType() == Door.DoorType.top) || (currDoor.getDoorType() == Door.DoorType.bottom)) 
            {
                if (currDoor.transform.position.y > player.transform.position.y) 
                {
                    player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1 + currDoor.GetHeight(), player.transform.position.z);
                } 
                else if (currDoor.transform.position.y < player.transform.position.y) 
                {
                    player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1 - currDoor.GetHeight(), player.transform.position.z);
                }
            }

            if ((currDoor.getDoorType() == Door.DoorType.left) || (currDoor.getDoorType() == Door.DoorType.right)) 
            {
                if (currDoor.transform.position.x > player.transform.position.x) 
                {
                    player.transform.position = new Vector3(player.transform.position.x + 1 + currDoor.GetLength(), player.transform.position.y, player.transform.position.z);
                } 
                else if (currDoor.transform.position.x < player.transform.position.x) 
                {
                    player.transform.position = new Vector3(player.transform.position.x - 1 - currDoor.GetLength(), player.transform.position.y, player.transform.position.z);
                }
            }

            // Activate cooldown after moving the player
            isPlayerMoving = true; // Set player movement flag
            doorCooldownTimer = doorCooldownTime; // Reset cooldown timer
        }
    }

    public Room getCurrRoom() 
    {
        return currRoom; // Get the current room
    }

    public void setRoom(Room room) 
    {
        currRoom = room; // Set the current room
    }

    public void SetCurrentDoor(Door door)
    {
        currDoor = door; // Set the current door
        // Debug.Log("Current door set to: " + currDoor.getDoorType());
    }
}
