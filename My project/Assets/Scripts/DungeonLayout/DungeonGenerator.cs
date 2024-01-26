using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms;

    private List<string> emptyRooms = new List<string> { "Empty", "Shop" };

    private void Start()
    {
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomController.instance.LoadRoom("Start", 0, 0);

        foreach (Vector2Int roomLocation in rooms)
        {
            string roomType;

            if (roomLocation == dungeonRooms[dungeonRooms.Count - 1] && roomLocation != Vector2Int.zero)
            {
                roomType = "End";
            }
            else
            {
                if (emptyRooms.Count == 0)
                {
                    roomType = "Empty";
                }
                else
                {
                    roomType = emptyRooms.RandomItem();
                    emptyRooms.Remove(roomType);
                }
            }

            RoomController.instance.LoadRoom(roomType, roomLocation.x, roomLocation.y);
            Debug.Log(roomType);
        }
    }
}

public static class ListExtensions
{
    public static T RandomItem<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}
