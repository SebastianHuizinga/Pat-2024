using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType { left, right, top, bottom }
    public DoorType doorType;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        // Initialize the SpriteRenderer reference
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is missing from the door.");
        }
    }

    public DoorType getDoorType()
    {
        return this.doorType; 
    }

    public float GetLength()
    {
        if (spriteRenderer != null)
        {
            return spriteRenderer.bounds.size.x; // Assuming the length is along the x-axis
        }
        return 0f;
    }

    public float GetHeight()
    {
        if (spriteRenderer != null)
        {
            return spriteRenderer.bounds.size.y; // Assuming the height is along the y-axis
        }
        return 0f;
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


