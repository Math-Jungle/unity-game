using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AccCreationToSetUp : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPasswordInput;

    // Add this reference in the Inspector
    public TMP_Text errorText;

    private void Start()
    {
        // Load saved email
        if (PlayerPrefs.HasKey("UserEmail"))
            emailInput.text = PlayerPrefs.GetString("UserEmail");

        // Load saved password
        if (PlayerPrefs.HasKey("UserPassword"))
        {
            string savedPassword = PlayerPrefs.GetString("UserPassword");
            passwordInput.text = savedPassword;
            confirmPasswordInput.text = savedPassword;

            // Keep the password fields in 'Password' mode
            passwordInput.contentType = TMP_InputField.ContentType.Password;
            confirmPasswordInput.contentType = TMP_InputField.ContentType.Password;
            passwordInput.ForceLabelUpdate();
            confirmPasswordInput.ForceLabelUpdate();
        }

        // Make sure error text starts empty
        errorText.text = "";
    }

    public void OnNextButtonClicked()
    {
        // Clear old error messages
        errorText.text = "";

        string email = emailInput.text.Trim();
        string password = passwordInput.text;
        string confirmPassword = confirmPasswordInput.text;

        // Validation checks
        if (string.IsNullOrEmpty(email))
        {
            errorText.text = "Please enter an email.";
            return;
        }

        if (!IsValidEmail(email))
        {
            errorText.text = "Invalid email address. Please include '@'.";
            return;
        }

        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            errorText.text = "Please enter and confirm your password.";
            return;
        }

        if (password != confirmPassword)
        {
            errorText.text = "Passwords do not match.";
            return;
        }

        // If all checks pass, save data and move on
        PlayerPrefs.SetString("UserEmail", email);
        PlayerPrefs.SetString("UserPassword", password);
        PlayerPrefs.Save();

        SceneTracker.LoadScene("setup");
    }

    private bool IsValidEmail(string email)
    {
        return email.Contains("@");
    }
}
