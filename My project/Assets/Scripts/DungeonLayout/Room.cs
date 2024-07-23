using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public int Width;
    public int Height;
    public int X;
    public int Y;

    public Door leftDoor;
     public Door rightDoor;
      public Door topDoor;
       public Door bottomDoor;
        public bool hasEntered = false;
      

       public List<Door> doors = new List<Door>();


    // Start is called before the first frame update
    void Start()
    {
        if(RoomController.instance == null){
            Debug.Log("Wrong scene");
            return;

        }

        Door[] ds = GetComponentsInChildren<Door>();
        foreach(Door d in ds){
            doors.Add(d);
            switch(d.doorType){
                case Door.DoorType.right:
                rightDoor = d;
                break;
                  case Door.DoorType.left:
                leftDoor = d;
                break;
                  case Door.DoorType.top:
                topDoor = d;
                break;
                  case Door.DoorType.bottom:
                bottomDoor = d;
                break;

            }
        }

        RoomController.instance.RegisterRoom(this);

    }
   
    public void RemoveUnconnectedDoors(){
        foreach(Door door in doors){
            switch(door.doorType){
                 case Door.DoorType.right:
                if(getRight() == null)
                    door.gameObject.SetActive(false);
                
                break;
                  case Door.DoorType.left:
               if(getLeft() == null)
                    door.gameObject.SetActive(false);
                
                break;
                  case Door.DoorType.top:
                if(getTop() == null)
                    door.gameObject.SetActive(false);
                
                break;
                  case Door.DoorType.bottom:
                if(getBottom() == null)
                    door.gameObject.SetActive(false);
                
                break;

            }

        }
    }

    public Room getRight(){
        if(RoomController.instance.DoesRoomExist(X + 1, Y))
        {
            return RoomController.instance.FindRoom(X + 1, Y);
        }
        return null;
    }
     public Room getLeft(){
        if(RoomController.instance.DoesRoomExist(X - 1, Y)){
            return RoomController.instance.FindRoom(X - 1, Y);
        }
        return null;
    }
     public Room getTop(){
        if(RoomController.instance.DoesRoomExist(X , Y + 1)){
            return RoomController.instance.FindRoom(X, Y + 1);
        }
        return null;
    }
     public Room getBottom(){
        if(RoomController.instance.DoesRoomExist(X, Y -1 )){
            return RoomController.instance.FindRoom(X , Y - 1);
        }
        return null;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }


    public Vector3 GetRoomCentre(){
        return new Vector3 ( X * Width, Y * Height);
    }
    public Vector3 findCentre(){
         return new Vector3(this.transform.position.x / Width, this.transform.position.y /Height); 
    }
  

void OnTriggerEnter2D(Collider2D other)
    {
     if (other.CompareTag("Player") && !hasEntered)
    {
        hasEntered = true;
            Debug.Log("Player entered room: " + name);
            RoomController.instance.OnPlayerEnterRoom(this); // Update the RoomController with the current room

        }
        
    }
}
