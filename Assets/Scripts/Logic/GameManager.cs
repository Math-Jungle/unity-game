using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Data Managers")]

    // [SerializeField] makes private variables visible in the Inspector
    [SerializeField] private LocalDataManager localDataManager;
    [SerializeField] private BackendDataManager backendDataManager;
    [SerializeField] private SyncManager syncManager;

    public GameData gameData;
    private bool isSubmittingResults = false;

    void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("GameManager instance created.");
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps GameManager alive across scenes
        }
        else
        {
            Debug.Log($"Duplicate GameManger detected in scene: {SceneManager.GetActiveScene().name}. Destroying duplicate.");
            Destroy(gameObject); // Prevents duplicate GameManagers
            return;
        }

        // Check for missing components
        if (localDataManager == null || backendDataManager == null || syncManager == null)
        {
            Debug.LogError("One or more manager components are missing on GameManager!");
        }

        // Initialize GameData as a singleton
        gameData = new GameData();
    }

    public void SubmitGameResults(int score, List<float> reactionTimes, float gameTime)
    {
        if (isSubmittingResults)
        {
            Debug.LogWarning("Already submitting game results. Ignoring duplicate request.");
            return;
        }

        isSubmittingResults = true;
        Debug.Log("SubmitGameResults called.");

        // Update GameData with the latest game results
        gameData.UpdateLevelData(SceneManager.GetActiveScene().name, score, reactionTimes, gameTime);
        Debug.Log("Game Data Submitted. Preparing to save and sync.");

        // Show the game over screen
        UIManager.instance.ShowGameOverScreen(score);

        // Store locally
        localDataManager.SaveGameData(gameData);

        // Store remotely
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            StartCoroutine(SendDatAndResetFlag(gameData));
        }
        else
        {
            isSubmittingResults = false;
        }

        // Sync if online
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            // FindObjectOfType<BackendDataManager>().SyncGameData(gameData);
        }
    }

    private IEnumerator SendDatAndResetFlag(GameData gameData)
    {
        yield return StartCoroutine(backendDataManager.SendGameData(gameData, ""));
        isSubmittingResults = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}