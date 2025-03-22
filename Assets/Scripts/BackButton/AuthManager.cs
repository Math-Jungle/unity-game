using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    // This method is called when the logout button is pressed.
    public void Logout()
    {
        // Remove the stored authentication token and any PlayerPrefs data used for user info.
        PlayerPrefs.DeleteKey("UserToken");
        PlayerPrefs.DeleteKey("ChildName");
        PlayerPrefs.DeleteKey("SelectedAvatar");
        PlayerPrefs.Save();

        // Optionally, clear any in-memory user data (e.g., in GameManager)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CurrentUserData = null;
        }

        // Delete the local JSON files for game and user data.
        string gameDataFilePath = Path.Combine(Application.persistentDataPath, "GameData.json");
        if (File.Exists(gameDataFilePath))
        {
            File.Delete(gameDataFilePath);
            Debug.Log("GameData.json deleted on logout.");
        }

        string userDataFilePath = Path.Combine(Application.persistentDataPath, "UserData.json");
        if (File.Exists(userDataFilePath))
        {
            File.Delete(userDataFilePath);
            Debug.Log("UserData.json deleted on logout.");
        }

        // Load the Login Scene.
        SceneManager.LoadScene("login");
    }
}
