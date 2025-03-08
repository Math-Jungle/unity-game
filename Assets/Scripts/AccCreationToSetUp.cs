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
        string email = emailInput.text.Trim();
        string password = passwordInput.text;
        string confirmPassword = confirmPasswordInput.text;

        if (IsValidEmail(email) && password == confirmPassword)
        {
            // Save email and password for later use
            PlayerPrefs.SetString("UserEmail", email);
            PlayerPrefs.SetString("UserPassword", password);
            PlayerPrefs.Save();

            // Ensure "setup" is the exact name of your child details scene.
            SceneManager.LoadScene("setup");
        }
        else
        {
            Debug.Log("Invalid input! Ensure the email is valid and both passwords match.");
        }
    }

    private bool IsValidEmail(string email)
    {
        return email.Contains("@");
    }
}
