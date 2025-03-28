using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [Header("Charts Controller")]
    [SerializeField] private ChartController chartsController;


    private GameData gameData;


    void Start()
    {
        // Get gameData from GameManager
        gameData = GameManager.Instance.GetGameData();

        // Initialize the charts
        if (chartsController != null)
        {
            chartsController.InitializeChart();
            chartsController.UpdateChartWithGameData(gameData);
            chartsController.InitializeGameTimeChart(gameData);
            chartsController.InitializeReactionTimeChart(gameData);
            //chartsController.InitializeGrowthChart(gameData);

        }
        else
        {
            Debug.LogError("Charts Controller is not assigned.");
        }
    }


}
