using System.Collections.Generic;
using UnityEngine;
using XCharts.Runtime;

public class ChartController : MonoBehaviour
{
    public LineChart chart;

    public void InitializeChart()
    {
        if (chart == null)
        {
            Debug.LogError("Chart is not assigned.");
            return;
        }

        // Clear any existing data
        chart.ClearData();

        // Ensure there's a line series
        if (chart.series.Count == 0)
        {
            chart.AddSerie<Line>("Score");
        }
    }

    public void UpdateChartWithGameData(GameData gameData)
    {
        if (chart == null)
        {
            Debug.LogError("Chart is not assigned.");
            return;
        }

        if (gameData == null || gameData.gameLevels == null || gameData.gameLevels.Count == 0)
        {
            Debug.Log("No game data available for chart.");
            return;
        }

        // Clear old data but keep the series
        chart.ClearData();

        // Ensure there's a line series
        if (chart.series.Count == 0)
        {
            chart.AddSerie<Line>("Score");
        }

        // Sort levels for chronological display
        List<GameLevel> sortedLevels = new List<GameLevel>(gameData.gameLevels);
        sortedLevels.Sort((a, b) => string.Compare(a.levelName, b.levelName));

        // Add data points
        foreach (var level in sortedLevels)
        {
            // Add X-axis label (use shorter labels like L1, L2, etc.)
            // You can customize how level names appear on the X-axis
            string displayName = level.levelName;
            if (displayName.Contains("Level"))
            {
                displayName = "L" + displayName.Replace("Level", "").Trim();
            }

            chart.AddXAxisData(displayName);

            // Add score data
            chart.AddData(0, level.score);
        }

        // Refresh the chart
        chart.RefreshChart();
    }
}