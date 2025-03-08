using UnityEngine;
using UnityEngine.SceneManagement;

public class EntrySceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckUserLoginStatus();
    }

    private void CheckUserLoginStatus()
    {
        string jwtToken = PlayerPrefs.GetString("UserToken", "");

        if (string.IsNullOrEmpty(jwtToken))
        {
            Debug.Log("User not logged in. Loading Splash Screen.");
            SceneManager.LoadScene("SplashScreen");
        }
        else
        {
            Debug.Log("User is logged in. Loading Home Page.");
            SceneManager.LoadScene("Home");
        }
    }
}
