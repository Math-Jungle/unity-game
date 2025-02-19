using UnityEngine;
using UnityEngine.SceneManagement;

public class splashToLogIn : MonoBehaviour
{
    public void LoadAccountCreation()
    {
        Debug.Log("Get Started button clicked!"); // Log button click
        Debug.Log("Loading acc creation scene...");

        SceneManager.LoadScene("login"); // Ensure "acc creation" is the exact scene name

        Debug.Log("Scene Loaded Successfully!"); // This may not appear if the scene load is instant
    }
}
