using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DashboardManager : MonoBehaviour
{
    private GameData gameData;

    [Header("UI Text Elements")]
    [SerializeField] private TextMeshProUGUI gameTimeText;
    [SerializeField] private TextMeshProUGUI gameLevelText;

    [Header("Chart Components")]
    [SerializeField] private ChartController scoreChartController;

    void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance not found.");
            return;
        }

        // Get game data
        gameData = GameManager.Instance.GetGameData();

        // Set dashboard UI elements
        SetGameLevel();
        SetGameTime();

        // Initialize and update the chart
        if (scoreChartController != null)
        {
            scoreChartController.InitializeChart();
            scoreChartController.UpdateChartWithGameData(gameData);
        }
        else
        {
            Debug.LogError("Score Chart Controller is not assigned.");
        }
    }

    private void SetGameTime()
    {
        // Set the game time
        float totalTime = gameData.GetTotalGameTime();
        if (gameTimeText != null)
        {
            gameTimeText.text = (totalTime / 60f).ToString("F0") + "min";
        }
    }

    private void SetGameLevel()
    {
        // Set the game level count
        if (gameLevelText != null && gameData.gameLevels != null)
        {
            gameLevelText.text = gameData.gameLevels.Count.ToString();
        }
    }

    // Method to refresh all dashboard data - call this if data changes
    public void RefreshDashboard()
    {
        // Update game data reference
        gameData = GameManager.Instance.GetGameData();

        // Update UI elements
        SetGameLevel();
        SetGameTime();

        // Update chart
        if (scoreChartController != null)
        {
            scoreChartController.UpdateChartWithGameData(gameData);
        }
    }

    // Navigation method
    public void Home()
    {
        SceneManager.LoadScene("Home");
    }
}