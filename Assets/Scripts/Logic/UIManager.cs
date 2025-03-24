using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [Header("UI Components")]
    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private GameObject pauseScreenCanvas;

    [Header("Loading Screen")]
    [SerializeField] private GameObject loadingScreenObject;
    [SerializeField] private Image loadingBarFill;
    private bool isLoading = false;

    void Awake()
    {
        // Signleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //ConfigureOrientation();
            SceneManager.sceneLoaded += OnSceneLoaded; // calls the OnSceneLoaded method everytime a new scene is loaded

            Debug.Log("UIManager instance created.");
        }
        else
        {
            Debug.Log($"Duplicate UIManager detected in scene: {SceneManager.GetActiveScene().name}. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Configure the orientation of the current scene
        ConfigureOrientation();
        // Hide all UI screens when a new scene is loaded
        HideUIScreens();
    }

    private void HideUIScreens()
    // Hide all UI screens
    {
        if (pauseScreenCanvas != null)
        {
            pauseScreenCanvas.SetActive(false);
        }

        if (gameOverScreen != null)
        {
            gameOverScreen.HideGameOverScreen();

        }

        Debug.Log("UI Screens hidden for new scene.");
    }

    public void ShowUIScreens()
    // Show needed UI elements in the game
    {
        // Shows the setting button for the pause screen
        if (pauseScreenCanvas != null)
        {
            pauseScreenCanvas.SetActive(true);
        }

    }

    public void ShowGameOverScreen(int score)
    {
        if (gameOverScreen == null)
        {
            Debug.LogError("GameOverScreen is not assigned.");
            return;
        }

        gameOverScreen.gameObject.SetActive(true);
        gameOverScreen.EndGame(score);
        Debug.Log($"Game Over Screen displayed. with score: {score}");
    }


    // Loaing Screen methods
    private IEnumerator LoadSceenAsync(string sceneName)
    {
        isLoading = true;

        if (loadingScreenObject == null)
        {
            Debug.LogError("Loading screen object is missing!");
            SceneManager.LoadScene(sceneName); // Incase the loading screen is missing, load the scene without it
            yield break;
        }

        // Show loadin screen
        loadingScreenObject.SetActive(true);

        // Start loading the scene  asynchronously
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false; // Wait untill the scene is fully loaded

        // Update the loading bar while the scene fully load
        while (operation.progress < 0.9f)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            if (loadingBarFill != null)
            {
                loadingBarFill.fillAmount = progressValue;
            }
            else
            {
                Debug.LogError("LoadingBarFill is not assigned.");
                yield return null;
            }
        }

        // Wait for a short time to show the full loading bar
        yield return new WaitForSeconds(0.5f);

        // Show 100% loading bar
        if (loadingBarFill != null)
        {
            loadingBarFill.fillAmount = 1;
        }

        // Allow the scene to be activated
        operation.allowSceneActivation = true;

        // Wait for the scene to fully activate
        yield return new WaitForSecondsRealtime(0.1f); // Endure minimum time for the scene to fully load
        yield return new WaitUntil(() => operation.isDone);  // Double-check if the scene is fully loaded
        // Both used since  operation.isDone is not always accurate and WaitForSecondsRealtime could be too short

        //Hide the loading screen
        loadingScreenObject.SetActive(false);
        isLoading = false;
    }

    private string GetNextSceneName(string currentScene, int score)
    {
        //string mode = (score > 8000) ? "-Hard" : ""; // If the score is greater than 8000, the next game will be in hard mode
        string mode = ""; // Use this untill Hard mde games are implemented

        switch (currentScene)
        {
            case "Game 1":
                return $"Sequence Number Game{mode}";
            case "Sequence Number Game":
                return $"BasketGame{mode}";
            case "BasketGame":
                return $"Game 3{mode}";
            case "Game 3":
                return $"Home";
            case "game5":
                return $"game6{mode}";
            case "game6":
                return "Home";
            default:
                return "Home";
        }
    }

    public void LoadNextGame(int prevGameScore = 0)
    {
        if (isLoading)
        {
            Debug.LogWarning("Already loading a scene. Ignoring duplicate request.");
            return;
        }

        string currentScene = SceneManager.GetActiveScene().name;
        string nextScene = GetNextSceneName(currentScene, prevGameScore);

        StartCoroutine(LoadSceenAsync(nextScene));
    }

    public void LoadScene(string sceneName)
    {
        if (isLoading)
        {
            Debug.LogWarning("Already loading a scene. Ignoring duplicate request.");
            return;
        }

        StartCoroutine(LoadSceenAsync(sceneName));
    }

    void OnDestroy()
    {
        // Important: Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Configure the orientation of the current scene that the game is in
    private void ConfigureOrientation()
    {
        // Get the current scene name
        string currentScene = SceneManager.GetActiveScene().name;

        // Set the orientation of the current scene
        switch (currentScene)
        {
            case "Startup":
            case "SplashScreen":
            case "Home":
            case "login":
            case "avatar":
            case "acc creation":
            case "Congratulation":
            case "Dashboard":
            case "game map":
            case "setup":
                Screen.orientation = ScreenOrientation.Portrait;
                break;
            case "Game 1":
            case "Sequence Number Game":
            case "BasketGame":
            case "Game 3":
            case "Game 4":
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                break;
            default:
                Screen.orientation = ScreenOrientation.AutoRotation;
                break;

        }
    }

}
