using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;

    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashLength = 0.2f;
    [SerializeField] float dashCD = 1f;
    bool isDashing;
    bool canDash = false; // Start with dashing disabled
    float dashCooldownTimer;

    public AbilityUI abilityUI; // Reference to the AbilityUI script

    void Start()
    {
        dashCooldownTimer = dashCD; // Initialize the cooldown timer
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
            canDash = false; // Disable dashing until cooldown is complete
            abilityUI.StartCooldown(dashCD); // Notify the AbilityUI to start the cooldown
        }

        if (!canDash)
        {
            ApplyCooldown();
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.MovePosition(rb.position + movement * dashSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;

        Vector2 dashDirection = movement.normalized;
        rb.velocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashLength);

        rb.velocity = Vector2.zero;
        isDashing = false;

        // Start the cooldown after the dash ends
        dashCooldownTimer = dashCD;
    }

    void ApplyCooldown()
    {
        if (!isDashing)
        {
            dashCooldownTimer -= Time.deltaTime;

            if (dashCooldownTimer <= 0f)
            {
                canDash = true; // Enable dashing again once cooldown is complete
                dashCooldownTimer = 0f; // Ensure timer does not go negative
            }
        }
    }
}