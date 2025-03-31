using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XCharts.Runtime;

public class GameMap : MonoBehaviour
{
    List<GameLevel> gameLevels;
    [SerializeField] private TextMeshProUGUI starCountText;

    [System.Serializable]
    private class GameButton
    {
        public string gameLevelName;
        public Button button;
        public StarDisplay starDisplay;
    }

    [Header("Game Buttons")]
    [SerializeField] private GameButton[] gameButtons;

    [Header("Star Thresholds")]
    [SerializeField] private int[] starThresholds = { 3000, 5000, 7000 };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameLevels = GameManager.Instance.GetGameData().gameLevels;

        InitializeGameButtons();
        SetStarCount();
    }

    private void InitializeGameButtons()
    {
        if (gameButtons == null || gameButtons.Length == 0)
        {
            Debug.LogError("Game buttons are not assigned or empty.");
            return;
        }

        foreach (GameButton gameButton in gameButtons)
        {
            if (gameButton == null || gameButton.button == null || gameButton.starDisplay == null)
            {
                Debug.LogError("Game button or its components are not assigned.");
                continue;
            }

            // Get the star count based on the score
            int starCount = GetStarCount(gameButton.gameLevelName);
            // Display stars
            gameButton.starDisplay.DisplayStars(starCount);
        }
    }

    private int GetStarCount(string levelName)
    {
        if (gameLevels == null || gameLevels.Count == 0)
        {
            Debug.LogError("There is no game Levels to get scores.");
            return 0;
        }
        // Find the game level by name
        GameLevel gameLevel = gameLevels.Find(level => level.levelName == levelName);

        if (gameLevel == null)
        {
            Debug.LogWarning("Game Level not found: " + levelName);
            return 0;
        }

        int score = gameLevel.score;

        // Calculate the star count based on the score
        if (score >= starThresholds[2])
        {
            return 3; // 3 stars
        }
        else if (score >= starThresholds[1])
        {
            return 2; // 2 stars
        }
        else if (score >= starThresholds[0])
        {
            return 1; // 1 star
        }
        else
        {
            return 0; // No stars
        }
    }

    private void SetStarCount()
    {
        // Set the star count
        if (starCountText != null)
        {
            int totalStars = GameManager.Instance.GetTotalStars();
            starCountText.text = totalStars.ToString();
        }
    }



    // Update is called once per frame
    void Update()
    {

    }



    // Buttons to load different game scenes
    public void Game1()
    {
        Debug.Log("Game 1 button Clicked");
        UIManager.instance.LoadScene("Game 1");
    }

    public void Game2()
    {
        Debug.Log("Game 2 button Clicked");
        UIManager.instance.LoadScene("Sequence Number Game");
    }

    public void Game3()
    {
        Debug.Log("Game 3 button Clicked");
        UIManager.instance.LoadScene("BasketGame");
    }

    public void Game4()
    {
        Debug.Log("Game 4 button Clicked");
        UIManager.instance.LoadScene("Game 3");
    }

    public void Game5()
    {
        Debug.Log("Game 5 button Clicked");
        UIManager.instance.LoadScene("ScaleGame");
    }

    public void Home()
    {
        Debug.Log("Home button Clicked");
        UIManager.instance.LoadScene("Home");
    }
}
