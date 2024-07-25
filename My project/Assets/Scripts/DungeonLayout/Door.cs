using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType { left, right, top, bottom }
    public DoorType doorType;
 
  public DoorType getDoorType(){
        return this.doorType; 
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            RoomController roomController = FindObjectOfType<RoomController>();
            if (roomController != null)
            {
                roomController.SetCurrentDoor(this); 
            }
        }
   
    }
    
}
