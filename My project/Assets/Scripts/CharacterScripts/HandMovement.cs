using System.Collections; // Required for using collections
using System.Collections.Generic; // Required for using generic collections like List
using UnityEngine; // Required for using Unity's core features

public class HandMovement : MonoBehaviour // Defines a class for controlling hand movement
{
    public Rigidbody2D rb; // Reference to the Rigidbody2D component
    public Camera cam; // Reference to the Camera component
    Vector2 mousePos; // Variable to store the mouse position

    void Update() // Unity's method called once per frame
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); // Converts mouse position from screen space to world space
    }

    void FixedUpdate() // Unity's method called at fixed intervals (used for physics updates)
    {
        Vector2 lookDir = mousePos - rb.position; // Calculate direction from the hand to the mouse position
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; // Calculate the angle to rotate towards
        rb.rotation = angle; // Set the Rigidbody2D's rotation to the calculated angle
    }
}
