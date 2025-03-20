using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    // This method is called when the logout button is pressed.
    public void Logout()
    {
        // Remove the stored authentication token.
        PlayerPrefs.DeleteKey("UserToken");
        PlayerPrefs.Save();

        // Optionally, clear any other user data or static variables here.
        // For example: UserProfile.Instance.Clear();

        // Load the Login Scene.
        SceneManager.LoadScene("login");
    }
}
