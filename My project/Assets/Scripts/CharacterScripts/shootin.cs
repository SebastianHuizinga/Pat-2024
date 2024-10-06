using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootin : MonoBehaviour
{
    private Camera cam; // Reference to the main camera
    private Vector3 mousePos; // Position of the mouse in world space

    // Start is called before the first frame update
    void Start()
    {
        // Get the main camera component using the tag
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse position in world coordinates
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position; // Calculate direction to the mouse
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg; // Calculate the rotation angle in degrees

        // Rotate the object to face the mouse position
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
