using UnityEngine;

public class gameManagement : MonoBehaviour
{
    public GameObject gameWinUI; // Reference to the scorecard popup
    public gameOverScreen gameOverScreen; // Reference to the gameOverScreen script

    // Method to show the scorecard popup
    public void GameWin()
    {
        Debug.Log("GameWin() called. Activating scorecard popup.");
        if (gameWinUI == null)
        {
            Debug.LogError("gameWinUI is not assigned!");
            return;
        }

        // Activate the scorecard popup
        if (gameWinUI.activeInHierarchy == false)
        {
            gameWinUI.SetActive(true);
            gameOverScreen.Start();
        }

        // Ensure the gameOverScreen script is assigned
        if (gameOverScreen == null)
        {
            Debug.LogError("gameOverScreen script is not assigned!");
            return;
        }
    }
}