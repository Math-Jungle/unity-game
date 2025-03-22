using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
<<<<<<< HEAD

=======
>>>>>>> Login-pages-Script

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Data Managers")]

    // [SerializeField] makes private variables visible in the Inspector
    [SerializeField] private LocalDataManager localDataManager;
    [SerializeField] private BackendDataManager backendDataManager;
    [SerializeField] private SyncManager syncManager;

    [Header("Startup Data")]
    // Add a game object to indicate app starting
    private bool isInitialized = false;


    private GameData gameData;
    private bool isSubmittingResults = false;

    public UserData CurrentUserData { get; set; }


    void Awake()
    {
        // Temporarily set tht jwt token
        //string jwtToken = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJwcmFqYW5zYS4yMDIzMTEzM0BpaXQuYWMubGsiLCJpYXQiOjE3NDIxMzc2NDUsImV4cCI6MTc0MjIyNDA0NX0.URpZ8SREGZR0Wo8TMVWS4Ud4YkP819zrDrpB1UsPpQw";
        //PlayerPrefs.SetString("AuthToken", jwtToken);
        //PlayerPrefs.Save();

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
        if (gameData == null)
        {
            gameData = new GameData();
            // Checking if the gameData object is already initialized since unity create a object because the  [SerializeField] creates a object in the inspector

        }
    }

    void Start()
    {
        // Check if this is the first scene load (Startup)
        if (!isInitialized && SceneManager.GetActiveScene().name == "Startup")
        {
            StartCoroutine(HandleAppStartup());
        }
    }

    private IEnumerator HandleAppStartup()
    {
        // Show loading indicator if available

        // Mark the app as initialized
        isInitialized = true;

        // Check if the JWT token is available
        string jwtToken = PlayerPrefs.GetString("AuthToken", "");
        bool tokenExists = !string.IsNullOrEmpty(jwtToken);

        Debug.Log($"Authentication check: Token {(tokenExists ? "found" : "not found")}.");

        // Check if the token is valid
        if (tokenExists && backendDataManager != null)
        {
            // Verify the JWT token
            yield return StartCoroutine(backendDataManager.VerifyToken(jwtToken));

            // After verifying the token, check if the token still exists (it would be deleted if the token is invalid)
            tokenExists = !string.IsNullOrEmpty(PlayerPrefs.GetString("AuthToken", ""));

        }
        if (tokenExists && Application.internetReachability != NetworkReachability.NotReachable)
        {
            // Try to load user data from local storage
            UserData localUserData = localDataManager.LoadUserData();
            if (localUserData == null)
            {
                // No local user data found; fetch from backend
                yield return StartCoroutine(backendDataManager.FetchUserData(jwtToken, (fetchedUserData) =>
                {
                    if (fetchedUserData != null)
                    {
                        Debug.Log("Fetched user data from backend: " + fetchedUserData.childName);
                        CurrentUserData = fetchedUserData;
                        localDataManager.SaveUserData(fetchedUserData);
                    }
                    else
                    {
                        Debug.LogWarning("Failed to fetch user data from backend.");
                    }
                }));
            }
            else
            {
                Debug.Log("User data loaded from local storage.");
                CurrentUserData = localUserData;
            }
        }

        // Sync data if authenicated and online
        if (tokenExists && Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (syncManager != null)
            {
                syncManager.SyncData(jwtToken);

                // Give sync time to complete
                yield return new WaitForSeconds(0.5f);
            }

            // Load the local game data
            localDataManager.LoadGameData();
        }

        // Determining which scene to load based on the token status
        string sceneToLoad = tokenExists ? "Home" : "SplashScreen";
        Debug.Log($"Starting application: Loading {sceneToLoad} scene.");

        // Load the scene
        SceneManager.LoadScene(sceneToLoad);

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
        string jwtToken = PlayerPrefs.GetString("AuthToken", "");

        yield return StartCoroutine(backendDataManager.SendGameData(gameData, jwtToken));
        isSubmittingResults = false;
    }


    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}