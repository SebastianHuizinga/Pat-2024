using UnityEngine; // Required for Unity functionalities
using System.Collections.Generic; // Required for List<T>
using System.Linq; // Required for LINQ methods
using System; // Required for LINQ methods like FirstOrDefault

public class PlayerShooting : MonoBehaviour // Class to handle player shooting behavior
{
    public GameObject bulletPrefab; // Bullet prefab to instantiate
    public Transform handTransform; // Assign this in the Inspector to the player's hand or weapon
    public float fireRate = 0.5f;   // Fire rate in seconds
    private float nextFireTime = 0f; // Time when the player can fire again
    private int ricochetCount = 0; // Store the number of ricochets available
    public bool isBurningBullet = false; // Flag to indicate if the bullet is a burning bullet
    public ScoreManager scoreManager; // Reference to the ScoreManager
    // Declare the delegate and event
    public delegate void OnShoot(GameObject bullet); // Delegate for shooting event
    public event OnShoot onShootEvent; // Event triggered when shooting

    // Reference to the user class (this should be assigned accordingly)
    public User user; // Assuming User class tracks bullets shot
    private string filename = "UserData.json"; // The filename for saving the user data

    void Start() // Unity's method called when the script instance is being loaded
    {
        if (handTransform == null) // Check if handTransform is assigned
        {
            // Find handTransform manually if it isn't assigned in the Inspector
            handTransform = transform.Find("Hand") ?? transform; // Adjust "Hand" to your actual GameObject name
            if (handTransform == null) // If handTransform is still null
            {
                Debug.LogError("HandTransform is not assigned! Please assign it in the Inspector."); // Log an error
            }
        }
    }

    void Update() // Unity's method called once per frame
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFireTime) // Check for fire input and fire rate
        {
            nextFireTime = Time.time + fireRate; // Set the next time the player can fire
            Shoot(); // Call the Shoot method
        }
    }

    void Shoot() // Method to handle shooting bullets
    {
        if (handTransform == null) // Check if handTransform is assigned
        {
            Debug.LogError("HandTransform is not assigned!"); // Log an error
            return; // Stop the function if handTransform is not set
        }

        if (bulletPrefab == null) // Check if bulletPrefab is assigned
        {
            Debug.LogError("BulletPrefab is not assigned!"); // Log an error
            return; // Stop the function if bulletPrefab is not set
        }

        GameObject bullet = Instantiate(bulletPrefab, handTransform.position, handTransform.rotation); // Instantiate the bullet

        Bullet bulletScript = bullet.GetComponent<Bullet>(); // Get the Bullet component from the instantiated bullet
        if (bulletScript != null) // Check if the bullet script is present
        {
            bulletScript.SetScoreManager(scoreManager); // Set the ScoreManager for the bullet
        }
        else // If bullet script is missing
        {
            Debug.LogError("Bullet script is missing on the bullet prefab!"); // Log an error
        }

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component from the bullet
        if (rb == null) // Check if Rigidbody2D is present
        {
            Debug.LogError("Rigidbody2D component is missing on the bullet prefab!"); // Log an error
            return; // Stop the function if Rigidbody2D is missing
        }
        rb.velocity = handTransform.up * 20f; // Set bullet velocity based on handTransform's up direction

        onShootEvent?.Invoke(bullet); // Trigger the onShootEvent if there are subscribers

        if (scoreManager != null) // Check if scoreManager is assigned
        {
            scoreManager.IncrementBulletsShot(); // Increment the bullets shot in ScoreManager
        }
        else // If scoreManager is not assigned
        {
            Debug.LogError("ScoreManager reference is not assigned!"); // Log an error
        }
    }

    // Method to set the ricochet ability from an item
    public void SetRicochetAbility(int count) // Method to set ricochet ability
    {
        ricochetCount = count; // Update the ricochet count
    }

    public void SetBurningBullet(bool value) // Method to set the burning bullet state
    {
        isBurningBullet = value; // Update the burning bullet flag
    }

    public void IncreaseFireRate(float increaseAmount) // Method to increase the fire rate
    {
        Debug.Log($"Increasing fire rate by {increaseAmount * 100}% from {fireRate}"); // Log the fire rate increase
        fireRate *= (1 - increaseAmount); // Decrease the time between shots
        fireRate = Mathf.Max(fireRate, 0.1f); // Clamp to a minimum value to prevent very high fire rates
        Debug.Log($"New fire rate: {fireRate}"); // Log the new fire rate
    }
}
