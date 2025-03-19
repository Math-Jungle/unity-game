using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AccCreationToSetUp : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPasswordInput;

    private void Start()
    {
        // Load saved email
        if (PlayerPrefs.HasKey("UserEmail"))
            emailInput.text = PlayerPrefs.GetString("UserEmail");

        // Load saved password (but don’t display the actual password)
        if (PlayerPrefs.HasKey("UserPassword"))
        {
            string savedPassword = PlayerPrefs.GetString("UserPassword");
            passwordInput.text = savedPassword;
            confirmPasswordInput.text = savedPassword;

            // Ensure the password fields stay in 'Password' mode
            passwordInput.contentType = TMP_InputField.ContentType.Password;
            confirmPasswordInput.contentType = TMP_InputField.ContentType.Password;

            // Refresh the InputField to apply the settings
            passwordInput.ForceLabelUpdate();
            confirmPasswordInput.ForceLabelUpdate();
        }
    }

    public void OnNextButtonClicked()
    {
        string email = emailInput.text.Trim();
        string password = passwordInput.text;
        string confirmPassword = confirmPasswordInput.text;

        if (IsValidEmail(email) && password == confirmPassword)
        {
            // Save user data
            PlayerPrefs.SetString("UserEmail", email);
            PlayerPrefs.SetString("UserPassword", password);
            PlayerPrefs.Save();

            // Load next scene
            SceneTracker.LoadScene("setup");
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
