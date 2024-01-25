using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    Vector2 movement;
    
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashLength = 1f;
    [SerializeField] float dashCD = 1f;
    bool isDashing;
    bool canDash;

/// <summary>
/// Start is called on the frame when a script is enabled just before
/// any of the Update methods is called the first time.
/// </summary>
void Start()
{
    canDash = true;
}
    void Update()
    {   
        if(isDashing){
            return;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash){
            StartCoroutine(Dash());
        }
       movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
                



    }

  
    void FixedUpdate()
    {
        if(isDashing){
            return;
        }

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        


    }

    private IEnumerator Dash(){
        isDashing = true;
        canDash = false;
        rb.velocity = new Vector2(movement.x * dashSpeed, movement.y * dashSpeed);
        yield return new WaitForSeconds(dashLength);
        isDashing = false;

        yield return new WaitForSeconds(dashCD);
        canDash = true;
    }
}
