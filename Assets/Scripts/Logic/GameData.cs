using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<GameLevel> gameLevels;
    public string lastUpdated;

    public GameData()
    {
        this.gameLevels = new List<GameLevel>();
        this.lastUpdated = System.DateTime.UtcNow.ToString();

        Debug.Log("New GameData created.");
    }

    // Update an existing level or add a new one
    public void UpdateLevelData(string levelName, int score, List<float> reactionTimes, float gameTime)
    {
        Debug.Log($"Updating GameData for level {levelName}");
        GameLevel existingLevel = gameLevels.Find(level => level.levelName == levelName); // Finds the GameLevel object that has the same level name as the one level currently inputting.

        if (existingLevel != null) // If the game level exist replace the data
        {
            existingLevel.score = score;
            existingLevel.reactionTimes = reactionTimes;
            existingLevel.gameTime = gameTime;
            existingLevel.timeStamp = System.DateTime.UtcNow.ToString("o");
            // No need to update LevelName because it already exists.
        }
        else
        {
            gameLevels.Add(new GameLevel(levelName, score, reactionTimes, gameTime));
        }

        lastUpdated = System.DateTime.UtcNow.ToString("o");

        Debug.Log($"GameData updated for level {levelName}");


    }

    public float GetTotalGameTime()
    {
        float totalGameTime = 0;

        foreach (GameLevel level in gameLevels)
        {
            totalGameTime += level.gameTime;
        }

        return totalGameTime;
    }

}
