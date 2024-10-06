using System.Collections; // Required for using collections
using UnityEngine; // Required for using Unity's core features

public class Player : MonoBehaviour // Defines a class for player behavior
{
    public float moveSpeed = 5f; // Movement speed of the player
    public Rigidbody2D rb; // Reference to the Rigidbody2D component
    Vector2 movement; // Variable to store movement input

    [SerializeField] float dashSpeed = 10f; // Speed during a dash
    [SerializeField] float dashLength = 0.2f; // Duration of the dash
    [SerializeField] float dashCD = 1f; // Cooldown duration for dashing
    bool isDashing; // Flag to check if the player is currently dashing
    bool canDash = false; // Flag to indicate if the player can dash
    float dashCooldownTimer; // Timer to track the dash cooldown
    public mainMenu mainmenu; // Reference to the main menu script
    public AbilityUI abilityUI; // Reference to the Ability UI script

    public float maxHealth = 100f; // Maximum health of the player
    private float currentHealth; // Current health of the player

    void Start() // Unity's method called when the script instance is being loaded
    {
        mainmenu = FindObjectOfType<mainMenu>();  // Find the mainMenu instance in the scene
        dashCooldownTimer = dashCD; // Initialize the cooldown timer
        currentHealth = maxHealth; // Set current health to maximum health
    }

    // Other methods... (placeholder for any other methods that might be added)

    void Update() // Unity's method called once per frame
    {
        if (isDashing) // If the player is currently dashing
        {
            return; // Skip the rest of the Update method
        }

        movement.x = Input.GetAxisRaw("Horizontal"); // Get horizontal input (left/right)
        movement.y = Input.GetAxisRaw("Vertical"); // Get vertical input (up/down)

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) // If LeftShift is pressed and the player can dash
        {
            StartCoroutine(Dash()); // Start the Dash coroutine
            canDash = false; // Disable dashing until cooldown
            abilityUI.StartCooldown(dashCD); // Start cooldown UI for the dash ability
        }

        if (!canDash) // If the player cannot dash
        {
            ApplyCooldown(); // Apply cooldown timer
        }
    }

    void FixedUpdate() // Unity's method called at fixed intervals (used for physics updates)
    {
        if (isDashing) // If the player is currently dashing
        {
            rb.MovePosition(rb.position + movement * dashSpeed * Time.fixedDeltaTime); // Move the player with dash speed
        }
        else // If the player is not dashing
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); // Move the player with normal speed
        }
    }

    private IEnumerator Dash() // Coroutine for handling dashing
    {
        isDashing = true; // Set the dashing flag to true

        Vector2 dashDirection = movement.normalized; // Normalize the movement direction for consistent dash direction
        rb.velocity = dashDirection * dashSpeed; // Set the Rigidbody2D's velocity for dashing

        yield return new WaitForSeconds(dashLength); // Wait for the duration of the dash

        rb.velocity = Vector2.zero; // Stop the player's movement after dashing
        isDashing = false; // Reset the dashing flag

        dashCooldownTimer = dashCD; // Reset the dash cooldown timer
    }

    void ApplyCooldown() // Method to apply cooldown for dashing
    {
        if (!isDashing) // If the player is not dashing
        {
            dashCooldownTimer -= Time.deltaTime; // Decrease the cooldown timer

            if (dashCooldownTimer <= 0f) // If the cooldown timer reaches 0
            {
                canDash = true; // Allow the player to dash again
                dashCooldownTimer = 0f; // Reset the cooldown timer
            }
        }
    }

    public void TakeDamage(float damage) // Method to apply damage to the player
    {
        currentHealth -= damage; // Decrease the player's current health
        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth); // Log the damage taken

        if (currentHealth <= 0f) // If the player's health drops to 0 or below
        {
            Die(); // Call the Die method
        }
    }

    private void Die() // Method to handle player death
    {
        mainmenu.ShowDeathPanel();  // Show the death panel and pause the game
        Debug.Log("Player has died."); // Log the death of the player
        Destroy(gameObject);  // Destroy the player GameObject (if desired)
    }

    public void Heal(float healAmount) // Method to heal the player
    {
        currentHealth += healAmount; // Increase the player's current health
        if (currentHealth > maxHealth) // If current health exceeds maximum health
        {
            currentHealth = maxHealth; // Set current health to maximum health
        }
        Debug.Log("Player healed. Current health: " + currentHealth); // Log the healing
    }

    public float GetCurrentHealth() // Method to get current health for UI
    {
        return currentHealth; // Return the player's current health
    }

    public void SetCurrentHealth(float hp) // Method to set current health from UI
    {
        currentHealth = hp; // Set the player's current health
    }
}
