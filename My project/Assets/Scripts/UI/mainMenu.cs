using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;  // Main menu panel
    public GameObject initiateGamePanel;
    public GameObject deathPanel;  // Death panel

    void Start()
    {
        // Pause the game by setting timeScale to 0 at the start
        Time.timeScale = 0;

        // Show the main menu panel, hide the game initiation panel and death panel by default
        mainMenuPanel.SetActive(true);
        initiateGamePanel.SetActive(false);
        deathPanel.SetActive(false);
    }

    // Function to resume the game and hide the main menu
    public void ResumeGame()
    {
        // Resume the game by setting timeScale to 1
        Time.timeScale = 1;
        mainMenuPanel.SetActive(false);  // Hide main menu
        initiateGamePanel.SetActive(false);  // Hide game initiation panel
    }

    // Function to show the game initiation panel and hide the main menu
    public void InitiateGame()
    {
        Time.timeScale = 0;  // Pause the game
        initiateGamePanel.SetActive(true);  // Show game initiation panel
    }

    // Function to return to the main menu
    public void ShowMainMenu()
    {
        Time.timeScale = 0;  // Pause the game
        mainMenuPanel.SetActive(true);  // Show main menu
        initiateGamePanel.SetActive(false);  // Hide game initiation panel
    }

    // Function to show the death panel
    public void ShowDeathPanel()
    {
        Time.timeScale = 0;  // Pause the game
        deathPanel.SetActive(true);  // Show death panel
        mainMenuPanel.SetActive(true);  // Hide main menu
        initiateGamePanel.SetActive(false);  // Hide game initiation panel
    }


     public void Restart()
    {
        // Get the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        // Reload the current scene
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}



