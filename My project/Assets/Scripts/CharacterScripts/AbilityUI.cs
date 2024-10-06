using System.Collections; // Required for using IEnumerator and coroutines
using System.Collections.Generic; // Required for using generic collections like List
using UnityEngine; // Required for using Unity's core features
using UnityEngine.UI; // Required for using UI components like Image

public class AbilityUI : MonoBehaviour // Defines a class for handling ability UI elements
{
    [Header("ability 1")] // Displays a header in the inspector for organization
    public Image AbilityImage1; // Reference to the UI image representing ability 1

    void Start() // Unity's method called when the script instance is being loaded
    {
        AbilityImage1.fillAmount = 0; // Initializes the fill amount of the ability image to 0 (not ready)
    }

    public void StartCooldown(float cooldownDuration) // Public method to start the cooldown process
    {
        StartCoroutine(CooldownCoroutine(cooldownDuration)); // Starts the cooldown coroutine
    }

    private IEnumerator CooldownCoroutine(float cooldownDuration) // Private coroutine for handling the cooldown
    {
        float cooldownTimer = cooldownDuration; // Initializes the timer with the cooldown duration
        float startTime = Time.time; // Records the start time (not used)

        while (cooldownTimer > 0f) // Loops until the cooldown timer reaches 0
        {
            cooldownTimer -= Time.deltaTime; // Decreases the timer by the time passed since the last frame
            AbilityImage1.fillAmount = cooldownTimer / cooldownDuration; // Updates the fill amount based on remaining cooldown
            yield return null; // Waits until the next frame before continuing the loop
        }

        AbilityImage1.fillAmount = 0f; // Resets the fill amount to 0 once cooldown is complete
    }
}
