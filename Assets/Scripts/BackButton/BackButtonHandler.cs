using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonHandler : MonoBehaviour
{
    public void OnBackButtonClicked()
    {
        if (PlayerPrefs.HasKey("PreviousScene"))
        {
            string previousScene = PlayerPrefs.GetString("PreviousScene");
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.LogWarning("No previous scene found! Assigning a default scene.");

           
            SceneManager.LoadScene("SplashScreen"); 
        }
    }
}
