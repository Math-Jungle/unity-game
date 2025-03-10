using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamesManager : MonoBehaviour
{
    public static GamesManager Instance { get; private set; }
    public int CurrentScore { get; private set; } = 0;
    public List<float> ReactionTimes { get; private set; } = new List<float>();
    public float TotalGameTime { get; private set; } = 0f;
    public string NextLevelDifficulty { get; private set; } = "Normal"; //Default difficulty
    public static string ActiveScene { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

        ActiveScene = SceneManager.GetActiveScene().name;
    }

    //Call this method from each game when it finishes to get the game data
    public void SubmitGameResults(int score, List<float> reactionTimes, float gameTime)
    {
        CurrentScore = score;
        ReactionTimes = new List<float>(reactionTimes);
        TotalGameTime = gameTime;
        NextLevelDifficulty = (score > 8000) ? "Hard" : "Normal"; // If (score > 8000) = true, "Hard" or "Normal"

        Debug.Log($"Game results submitted: {score} points, {gameTime} seconds. \n Next level difficulty is: {NextLevelDifficulty}");
    }

    //This method stores game data locally using PlayerPrefs
    public void StoreGameData()
    {
        PlayerPrefs.SetInt("LastGameScore", CurrentScore);
        PlayerPrefs.SetFloat("LastGameTime", TotalGameTime);

        //Converting Reaction times list to a comma-seperated string
        string reactionTimesStr = string.Join(",", ReactionTimes);
        PlayerPrefs.SetString("LastGameReactionTimes", reactionTimesStr);

        PlayerPrefs.Save();

        Debug.Log("Game data stored locally.");
    }

    //Method to trigger the game over process
    public void EndCurrentGame()
    {
        Debug.Log("Ending current game...");

        gameOverScreen gameOverScreen = FindFirstObjectByType<gameOverScreen>();
        if (gameOverScreen != null)
        {
            gameOverScreen.SetScore(CurrentScore);
        }
        else
        {
            Debug.LogError("GameOverScreen not found in the scene.");
        }


    }
}