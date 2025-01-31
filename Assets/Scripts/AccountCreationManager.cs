using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AccountCreationManager : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPasswordInput;

    public void CreateAccount()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        string confirmPassword = confirmPasswordInput.text;

        if (IsValidAccount(email, password, confirmPassword))
        {
            PlayerPrefs.SetString("SavedEmail", email);  // Store email (for later use in login)
            PlayerPrefs.SetString("SavedPassword", password);  // Store password (for login validation)
            PlayerPrefs.Save();

            Debug.Log("Account created successfully! Redirecting to Login...");
            SceneManager.LoadScene("login");  // Ensure your login scene is named correctly
        }
        else
        {
            Debug.LogError("Invalid input! Make sure all fields are correctly filled.");
        }
    }

    private bool IsValidAccount(string email, string password, string confirmPassword)
    {
        return !string.IsNullOrWhiteSpace(email) &&
               !string.IsNullOrWhiteSpace(password) &&
               password == confirmPassword;
    }
}
