using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    public static void LoadScene(string sceneName)
    {
        // Store the current scene before switching
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        // Load the new scene
        SceneManager.LoadScene(sceneName);
    }
}
