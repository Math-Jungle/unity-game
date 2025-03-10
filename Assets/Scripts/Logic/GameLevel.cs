using System.Collections.Generic;

[System.Serializable]
public class GameLevel
{
    public string levelName;  // Properties should start with capital
    public int score;
    public List<float> reactionTimes;
    public float gameTime;
    public string timeStamp;


    public GameLevel(string levelName, int score, List<float> reactionTimes, float gameTime)
    {
        this.levelName = levelName;
        this.score = score;
        this.reactionTimes = reactionTimes;
        this.gameTime = gameTime;
        this.timeStamp = System.DateTime.UtcNow.ToString("o");  // Use ISO8601 format
    }

}
