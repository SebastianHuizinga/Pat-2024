using System.Collections;
using UnityEngine;

public class BurningDamage : MonoBehaviour
{
    // Damage dealt per second by the burning effect
    public float burnDamage = 1f; 

    // Method to initiate the burn damage application over a specified duration
    public void ApplyBurn(float duration)
    {
        // Start the coroutine to handle burn damage over time with the given duration
        StartCoroutine(DoBurnDamage(duration)); 
    }

    // Coroutine to apply burn damage at regular intervals for a specified duration
    private IEnumerator DoBurnDamage(float duration)
    {
        float timer = duration; // Set up a timer for the burn duration

        // Continue applying damage while the timer is greater than zero
        while (timer > 0)
        {
            // Apply burn damage to the enemy using its TakeDamage method
            GetComponent<Enemy>().TakeDamage(burnDamage);
            timer -= 1f; // Decrease the timer by 1 second
            // Wait for 1 second before applying damage again
            yield return new WaitForSeconds(1f);
        }
    }
}
