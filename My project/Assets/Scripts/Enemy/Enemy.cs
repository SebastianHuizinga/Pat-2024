using System.Collections;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;            // Speed of the enemy
    public float health = 50f;          // Health of the enemy
    public float burnDamage = 1f;       // Damage per second from burning
    public float burnDuration = 5f;     // Duration of the burn effect
    public float poisonDamage = 1f;     // Damage per second from poison
    public float poisonDuration = 5f;   // Duration of the poison effect
    public float contactDamage = 10f;   // Damage dealt to the player on contact
    public float knockbackForce = 5f;   // Force applied to the enemy when it contacts the player
    private float burnTimer = 0f;       // Timer for burn effect
    private float poisonTimer = 0f;     // Timer for poison effect
    private bool isBurning = false;     // Is the enemy currently burning
    private bool isPoisoned = false;    // Is the enemy currently poisoned
    private bool isSlowed = false;      // Is the enemy currently slowed
    private Room currentRoom;           // Reference to the enemy's current room
    private Room playerRoom;      
    private bool isKnockedBack = false;   // To track if enemy is being knocked back
    private float knockbackDuration = 0.1f;
    // Duration of the knockback effect      // Reference to the player's current room
    public GameObject coinPrefab;       // Reference to the coin prefab
    public float dropChance = 0.5f;     // 50% chance to drop a coin
    private Rigidbody2D rb;             // Rigidbody reference for physics interaction
     
   
    private void Die()
    {
        DropCoin();
      
        Destroy(gameObject);
    }

 



    void Start()
{
    rb = GetComponent<Rigidbody2D>();
    rb.gravityScale = 0; // Disable gravity for top-down movement
    rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Prevent rotation due to forces
}

   void Update()
{
    GameObject player = GameObject.FindWithTag("Player");

    // Update playerRoom from RoomController (assuming RoomController has the current room)
    if (RoomController.instance != null && player != null)
    {
        playerRoom = RoomController.instance.getCurrRoom(); // Get the player's current room

        // Check if both enemy and player are in the same room
        if (currentRoom == playerRoom)
        {
            if (!isKnockedBack)  // Only chase if not knocked back
            {
                ChasePlayer(player);  // Chase if both are in the same room
            }
        }
    }

    // Handle burning and poisoning timers
    HandleStatusEffects();
}

    private void ChasePlayer(GameObject player)
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

     //   Debug.Log("Chasing player: " + player.name);  // Debugging

        // Move towards the player
        transform.position += direction * speed * Time.deltaTime;
       // Debug.Log("Moving towards player.");  // Debugging
    }

private void OnCollisionEnter2D(Collision2D collision)
{
    Debug.Log("Collided with: " + collision.gameObject.name);

    // Check if the colliding object is the Player
    if (collision.gameObject.CompareTag("Player"))
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(contactDamage);
        }

        // Apply knockback to the enemy (if you want enemies to be knocked back by player contact)
        Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized; // Calculate direction away from the player
        StartCoroutine(ApplyKnockback(knockbackDirection));
    }
    // Check if collided with a bullet
    else if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
    {
        // Handle bullet collision
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            // Ignore physical collision from bullets
            Debug.Log("Collision ignored from bullet: " + collision.gameObject.name);
            return; // Exit the method early, preventing knockback or movement changes
        }
    }

    // Optional: Handle other collisions here if needed
}








    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider is on the "Default" layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            currentRoom = other.GetComponent<Room>(); // Set the current room when the enemy enters a room
           // Debug.Log("Enemy entered room: " + currentRoom.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the other collider is on the "Default" layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            currentRoom = null;  // Reset room reference when the enemy exits
           // Debug.Log("Enemy exited room.");
        }
    }

public void TakeDamage(float damage)
{
    health -= damage;

    // Reset enemy velocity to ensure they start moving again
    rb.velocity = Vector2.zero; // Reset the velocity to avoid any unintended movements

    // Start chasing the player immediately after taking damage
    GameObject player = GameObject.FindWithTag("Player");
    if (player != null && RoomController.instance != null)
    {
        Room playerRoom = RoomController.instance.getCurrRoom(); // Get the player's current room

        // Check if both enemy and player are in the same room
        if (currentRoom == playerRoom)
        {
            ChasePlayer(player);  // Initiate chase
        }
    }

    if (health <= 0f)
    {
        Die();
    }
}



 

 private void DropCoin()
{
    // Use UnityEngine.Random to avoid ambiguity
    if (UnityEngine.Random.Range(0f, 1f) <= dropChance)
    {
        Instantiate(coinPrefab, transform.position, Quaternion.identity);
    }
}
   public void ApplyBurn(float duration)
{
    if (!isBurning)
    {
        isBurning = true;
        burnTimer = duration;
        Debug.Log("Burn applied for " + duration + " seconds.");
        StartCoroutine(DoBurnDamage());
    }
}


    public void ApplyPoison(float duration)
    {
        if (!isPoisoned)
        {
            isPoisoned = true;
            poisonTimer = duration;
            StartCoroutine(DoPoisonDamage());
        }
    }

    public void ApplySlow(float duration)
    {
        if (!isSlowed)
        {
            StartCoroutine(SlowCoroutine(duration));
        }
    }

    private IEnumerator SlowCoroutine(float duration)
    {
        isSlowed = true;
        float originalSpeed = speed;
        speed *= 0.5f;  // Reduce speed by half

        yield return new WaitForSeconds(duration);

        speed = originalSpeed;  // Restore original speed
        isSlowed = false;
    }

 
private IEnumerator DoBurnDamage()
{
    while (isBurning && burnTimer > 0)
    {
        Debug.Log("Burn damage dealt: " + burnDamage);
        TakeDamage(burnDamage);
        burnTimer -= 1f; // Decrease the burn timer by 1 second
        yield return new WaitForSeconds(1f);
    }
    isBurning = false; // Reset burn status
}

    private IEnumerator DoPoisonDamage()
    {
        while (isPoisoned && poisonTimer > 0)
        {
            TakeDamage(poisonDamage);
            poisonTimer -= 1f; // Decrease the poison timer by 1 second
            yield return new WaitForSeconds(1f);
        }
        isPoisoned = false; // Reset poison status
    }

    private void HandleStatusEffects()
    {
        // This method could be used to handle additional logic related to status effects
        if (isBurning && burnTimer <= 0)
        {
            isBurning = false; // Reset burn status if timer ends
        }
        if (isPoisoned && poisonTimer <= 0)
        {
            isPoisoned = false; // Reset poison status if timer ends
        }
    }
private IEnumerator ApplyKnockback(Vector2 direction)
{
    isKnockedBack = true;
    rb.velocity = Vector2.zero; // Reset any previous velocity
    float adjustedKnockbackForce = knockbackForce * 0.2f; // Adjusted knockback force

    rb.AddForce(direction * adjustedKnockbackForce, ForceMode2D.Impulse); // Apply the knockback

    yield return new WaitForSeconds(knockbackDuration); // Wait for the knockback duration

    rb.velocity = Vector2.zero; // Reset velocity to prevent floating
    isKnockedBack = false; // Knockback is over

    // Resume chasing the player after knockback
    ResumeChasingPlayer();
}




private void ResumeChasingPlayer()
{
    GameObject player = GameObject.FindWithTag("Player");

    // Check if the player is not null and is in the same room
    if (player != null && RoomController.instance != null)
    {
        Room playerRoom = RoomController.instance.getCurrRoom(); // Get the player's current room

        // Check if both enemy and player are in the same room
        if (currentRoom == playerRoom)
        {
            ChasePlayer(player);  // Resume chasing the player
        }
    }
}



    public float GetHealth()
{
    return health;
}


}