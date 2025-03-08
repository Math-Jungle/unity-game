[System.Serializable]
public class GameLevel
{
    public string LevelName { get; set; } // Properties should start with capital
    public int Score { get; set; }
    public float[] ReactionTimes { get; set; }
    public float GameTime { get; set; }
    public string TimeStamp { get; set; }


    public GameLevel(string levelName, int score, float[] reactionTimes, float gameTime)
    {
        this.LevelName = levelName;
        this.Score = score;
        this.ReactionTimes = reactionTimes;
        this.GameTime = gameTime;
        this.TimeStamp = System.DateTime.UtcNow.ToString("o");  // Use ISO8601 format
    }

}
