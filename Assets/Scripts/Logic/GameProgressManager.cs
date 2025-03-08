using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    public void SaveLevelData(string levelName, int score, float[] reactionTimes, float gameTime)
    {
        GameData gameData = GameManager.Instance.localDataManager.LoadGameData();

        if (gameData == null)
        {
            Debug.Log("Level haven't been played before. Creating new data.");
            gameData = new GameData();
        }

        gameData.UpdateLevelData(levelName, score, reactionTimes, gameTime);
        GameManager.Instance.localDataManager.SaveGameData(gameData);

        Debug.Log($"Saved progress for level {levelName} with score {score}");
    }
}
