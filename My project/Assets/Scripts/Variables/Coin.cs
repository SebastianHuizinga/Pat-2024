using UnityEngine;

public class Coin : MonoBehaviour
{
    private Money ms; // Reference to the Money script

    void Start()
    {
        // Initialize the Money script reference
        ms = FindObjectOfType<Money>();

        if (ms == null)
        {
            Debug.LogError("Money script not found in the scene.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (ms != null)
            {
                ms.AddCurrency(); // Add 12 currency units
                Destroy(gameObject); // Destroy the coin after collecting it
            }
        }
    }
}
