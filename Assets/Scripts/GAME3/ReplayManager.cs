using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public void ResetGame()
    {
        foreach (AppleFallGame3 apple in AppleFallGame3.allApples)
        {
            apple.ResetApple();
        }

        // Reset click counters
        AppleFallGame3.ResetCounts();
    }
}
