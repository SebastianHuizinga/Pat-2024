using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;

    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashLength = 1f;
    [SerializeField] float dashCD = 1f;
    bool isDashing;
    bool canDash = true;
    float dashCooldownTimer;

    void Start()
    {
        canDash = true;
        dashCooldownTimer = 0f;
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (!canDash)
        {
            ApplyCooldown();
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        rb.velocity = new Vector2(movement.x * dashSpeed, movement.y * dashSpeed);
        yield return new WaitForSeconds(dashLength);
        isDashing = false;

        // Start the cooldown timer
        dashCooldownTimer = dashCD;
    }

    void ApplyCooldown()
    {
        dashCooldownTimer -= Time.deltaTime;
        if (dashCooldownTimer <= 0f)
        {
            canDash = true;
        }
    }
}