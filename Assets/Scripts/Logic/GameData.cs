using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<GameLevel> GameLevels { get; set; }
    public string LastUpdated { get; set; }

    public GameData()
    {
        this.GameLevels = new List<GameLevel>();
        this.LastUpdated = System.DateTime.UtcNow.ToString();

        Debug.Log("New GameData created.");
    }

    // Update an existing level or add a new one
    public void UpdateLevelData(string levelName, int score, List<float> reactionTimes, float gameTime)
    {
        Debug.Log($"Updating GameData for level {levelName}");
        GameLevel existingLevel = GameLevels.Find(level => level.LevelName == levelName); // Finds the GameLevel object that has the same level name as the one level currently inputting.

        if (existingLevel != null) // If the game level exist replace the data
        {
            existingLevel.Score = score;
            existingLevel.ReactionTimes = reactionTimes;
            existingLevel.GameTime = gameTime;
            existingLevel.TimeStamp = System.DateTime.UtcNow.ToString("o");
            // No need to update LevelName because it already exists.
        }
        else
        {
            GameLevels.Add(new GameLevel(levelName, score, reactionTimes, gameTime));
        }

        LastUpdated = System.DateTime.UtcNow.ToString("o");

        Debug.Log($"GameData updated for level {levelName}");


    }

}
