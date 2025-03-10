using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.tvOS;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public LocalDataManager localDataManager;
    public BackendDataManager backendDataManager;
    public SyncManager syncManager;
    public GameOverScreen gameOverScreen;
    public GameData GameData { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps GameManager alive across scenes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicate GameManagers
            return;
        }

        // Ensure managers are assigned
        localDataManager = GetComponent<LocalDataManager>();
        backendDataManager = GetComponent<BackendDataManager>();
        syncManager = GetComponent<SyncManager>();

        if (localDataManager == null || backendDataManager == null || syncManager == null)
        {
            Debug.LogError("One or more manager components are missing on GameManager!");
        }

        // Initialize GameData as a singleton
        GameData = new GameData();
    }

    public void SubmitGameResults(int score, List<float> reactionTimes, float gameTime)
    {
        Debug.Log("SubmitGameResults called.");

        // Update GameData with the latest game results
        GameData.UpdateLevelData(SceneManager.GetActiveScene().name, score, reactionTimes, gameTime);
        Debug.Log("Game Data Submitted. Preparing to save and sync.");

        // Trigger Game Over
        gameOverScreen.EndGame(score);

        // Store locally
        foreach (GameLevel level in GameData.GameLevels)
        {
            Debug.Log($"Level: {level.LevelName}, Score: {level.Score}, Time: {level.GameTime}");
        }
        Debug.Log(GameData.LastUpdated);
        localDataManager.SaveGameData(GameData);

        // Try to store remotely, but don't let errors break the game flow
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            StartCoroutine(backendDataManager.SendGameData(GameData, ""));
        }

        // Sync if online
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            // FindObjectOfType<BackendDataManager>().SyncGameData(gameData);
        }
    }


}
