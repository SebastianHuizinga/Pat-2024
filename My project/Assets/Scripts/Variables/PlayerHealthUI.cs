using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    public Player player;              // Reference to the player script
    public TextMeshProUGUI healthText; // TextMeshProUGUI for the health display

    void Start()
    {
        // Initialize the health text display at the start of the game
        UpdateHealthText();
    }

    void Update()
    {
        // Continuously update the health text display in real-time
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        // Update the text with the player's current health
        healthText.text = "Health: " + player.GetCurrentHealth().ToString("F0");
    }
}
