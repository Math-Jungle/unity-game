[System.Serializable]
public class GameData
{
    public string gameLevel;
    public int score;
    public float[] reactionTimes;
    public float gameTime;
    public string timestamp;

    public GameData(string gameLevel, int score, float[] reactionTimes, float gameTime)
    {
        this.gameLevel = gameLevel;
        this.score = score;
        this.reactionTimes = reactionTimes;
        this.gameTime = gameTime;
        this.timestamp = System.DateTime.UtcNow.ToString("o");  // Use ISO8601 format

    }

}
