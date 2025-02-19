using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AccCreationToSetUp : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPasswordInput;

    public void OnNextButtonClicked()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        string confirmPassword = confirmPasswordInput.text;

        if (IsValidEmail(email) && password == confirmPassword)
        {
            SceneManager.LoadScene("setup"); // Ensure "setup" is the correct scene name
        }
        else
        {
            Debug.Log("Invalid input! Ensure email is correct and passwords match.");
        }
    }

    private bool IsValidEmail(string email)
    {
        return email.Contains("@");
    }
}

