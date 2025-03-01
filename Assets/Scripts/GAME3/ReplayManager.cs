using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public void ResetApples()
{
    Debug.Log("Replay Button Clicked! Resetting Apples...");

    // Find all apples in the scene
    AppleFallGame3[] allApples = FindObjectsOfType<AppleFallGame3>();
    
    // Reset each apple to its original position
    foreach (AppleFallGame3 apple in allApples)
    {
        apple.ResetApple();
    }
}

}
