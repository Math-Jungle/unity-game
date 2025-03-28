using System.Collections.Generic;
using UnityEngine;
using XCharts.Runtime;
using System.Linq;

public class ChartController : MonoBehaviour
{
    public LineChart chart;
    [SerializeField] private LineChart gameTimeChart;
    [SerializeField] private LineChart reactionTimeChart;
    [SerializeField] private LineChart GrowthChart;

    public void InitializeGameTimeChart(GameData gameData)
    {
        if (gameTimeChart == null)
        {
            Debug.LogError("Game Time Chart is not assigned.");
            return;
        }

        if (gameData == null || gameData.gameLevels == null || gameData.gameLevels.Count == 0)
        {
            Debug.Log("No game data available for chart.");
            return;
        }

        // Clear any existing data
        gameTimeChart.ClearData();

        // Ensure there's a line series
        if (gameTimeChart.series.Count == 0)
        {
            gameTimeChart.AddSerie<Line>("Game Time");
        }

        // Sort the levels for chronological display
        List<GameLevel> sortedLevels = new List<GameLevel>(gameData.gameLevels);
        sortedLevels.Sort((a, b) => string.Compare(a.levelName, b.levelName));

        // Add data points
        foreach (GameLevel level in sortedLevels)
        {
            // Add X-axis label
            string displayName = level.levelName;
            if (displayName.Contains("Level"))
            {
                displayName = "L" + displayName.Replace("Level", "").Trim();
            }

            gameTimeChart.AddXAxisData(displayName);
            gameTimeChart.AddData(0, level.gameTime); // Add the game time data point 
        }

        // Refresh the chart
        gameTimeChart.RefreshChart();

    }

    public void InitializeReactionTimeChart(GameData gameData)
    {
        if (reactionTimeChart == null)
        {
            Debug.LogError("Game Time Chart is not assigned.");
            return;
        }

        if (gameData == null || gameData.gameLevels == null || gameData.gameLevels.Count == 0)
        {
            Debug.Log("No game data available for chart.");
            return;
        }

        // Clear any existing data
        reactionTimeChart.ClearData();

        // Ensure there's a line series
        if (reactionTimeChart.series.Count == 0)
        {
            reactionTimeChart.AddSerie<Line>("Reaction Time");
        }

        // Sort the levels for chronological display
        List<GameLevel> sortedLevels = new List<GameLevel>(gameData.gameLevels);
        sortedLevels.Sort((a, b) => string.Compare(a.levelName, b.levelName));

        // Add data points
        foreach (GameLevel level in sortedLevels)
        {
            // Add X-axis label
            string displayName = level.levelName;
            if (displayName.Contains("Level"))
            {
                displayName = "L" + displayName.Replace("Level", "").Trim();
            }

            reactionTimeChart.AddXAxisData(displayName);
            float avgReactionTime = level.reactionTimes.Count > 0 ? level.reactionTimes.Average() : 0f;
            reactionTimeChart.AddData(0, avgReactionTime); // Add the game time data point 
        }

        // Refresh the chart
        reactionTimeChart.RefreshChart();

    }

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