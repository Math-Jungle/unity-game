using UnityEngine;
using UnityEngine.SceneManagement;

public class splashToLogIn : MonoBehaviour
{
    // This function is linked to the Get Started button.
    public void LoadAccountCreation()
    {
        Debug.Log("Get Started button clicked!");
        Debug.Log("Loading account creation scene...");
        // Ensure "login" is the exact name of your account creation scene.
        SceneManager.LoadScene("login");
        Debug.Log("Scene Loaded Successfully!");
    }
}
