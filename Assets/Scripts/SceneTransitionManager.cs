using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Required for TextMeshPro input fields

public class SceneTransitionManager : MonoBehaviour
{
    public TMP_InputField usernameInput;  // Reference for the username input field
    public TMP_InputField passwordInput;  // Reference for the password input field

    public void LoadSetupScene()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (IsValidLogin(username, password))
        {
            Debug.Log($"Login successful! Username: {username}");
            SceneManager.LoadScene("setup"); // Ensure "setup" matches the actual scene name
        }
        else
        {
            Debug.LogError("Invalid username or password!"); // You can show this in the UI
        }
    }

    private bool IsValidLogin(string username, string password)
    {
        // Simple validation: Check if fields are not empty
        return !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password);
    }
}
