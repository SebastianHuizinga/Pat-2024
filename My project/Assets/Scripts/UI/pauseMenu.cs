using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management
using UnityEngine.UI; // Needed for UI

public class pauseMenu : MonoBehaviour
{
    public GameObject menuPanel; // Assign the Panel here in the Inspector

    private void Start()
    {
        menuPanel.SetActive(false); // Ensure the panel is hidden at the start
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        // Toggle the menu visibility
        menuPanel.SetActive(!menuPanel.activeSelf);
        Time.timeScale = menuPanel.activeSelf ? 0 : 1; // Pause the game if the menu is active
    }

    public void ResumeGame()
    {
        menuPanel.SetActive(false); // Hide the menu panel
        Time.timeScale = 1; // Resume the game
    }

    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1; // Ensure the game is running
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the application
    }
}
