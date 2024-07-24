using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo {

    public string name;
    public int X;
    public int Y;
  
   
}


public class RoomController : MonoBehaviour
{

    
  public GameObject player;
    public static RoomController instance;

    string currentWorldName = "";

    RoomInfo currentLoadRoomData;
    Room currRoom;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;



    void Awake()
    {
        instance = this;
    }


    void Start(){


      
    }

    void Update(){

        UpdateRoomQueue();

    }

    void UpdateRoomQueue(){

        if(isLoadingRoom){
            return;
        }
        if(loadRoomQueue.Count == 0){
            return;
        }
        currentLoadRoomData= loadRoomQueue.Dequeue();
        isLoadingRoom = true;

    StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }
    public void LoadRoom(string name, int x, int y){

        if(DoesRoomExist(x , y)){
            return;
        }
            RoomInfo newRoomData = new RoomInfo();
            newRoomData.name = name;
            newRoomData.X = x;
            newRoomData.Y = y;

            loadRoomQueue.Enqueue(newRoomData);

    }

    IEnumerator LoadRoomRoutine(RoomInfo info){
        string roomName = currentWorldName + info.name;

         AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while(loadRoom.isDone == false){
            yield return null;

        }
    }


  
             public void RegisterRoom(Room room){
               if(!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y)){
        room.transform.position = new Vector3(

            currentLoadRoomData.X * room.Width, 
            currentLoadRoomData.Y * room.Height, 0);


            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;

             if(loadedRooms.Count == 0){

            // CameraController.instance.currRoom = room;
         }

            loadedRooms.Add(room);
            room.RemoveUnconnectedDoors();
    } else
    {

        Destroy(room.gameObject);
        isLoadingRoom = false;
    }
        }

    

    public bool DoesRoomExist (int x, int y){
        return loadedRooms.Find( item => item.X == x && item.Y == y) != null;

    }
    public Room FindRoom (int x, int y){
        return loadedRooms.Find( item => item.X == x && item.Y == y);

    }

    public void OnPlayerEnterRoom(Room room){
        currRoom = room;
        //CameraController.instance.Update(currRoom.findCentre);
        Debug.Log("Current pos" + currRoom.findCentre());
        player.transform.position = currRoom.findCentre();
         
    }
    public Room getCurrRoom(){
        return currRoom;
    }
    public void setRoom(Room room){
        currRoom = room;
    }
}

  
    
