using UnityEngine;
using UnityEngine.SceneManagement; // Import scene management

public class SceneLoader : MonoBehaviour
{
    public void LoadAccCreationScene()
    {
        Debug.Log("Get Started button clicked! Loading Account Creation Scene...");
        SceneManager.LoadScene("acc creation"); // Ensure this matches the scene name exactly
    }
}
