using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;
using System.Text;

public class SignupClickHandler : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button forgotPasswordButton;
    [SerializeField] private Button signUpButton;
    [SerializeField] private Button googleLoginButton;

    [Header("Backend URL")]
    [SerializeField] private string loginEndpoint = "https://spring-app-249115746984.asia-south1.run.app/user/login";

    private void Start()
    {
        loginButton.onClick.AddListener(OnLoginClicked);
        forgotPasswordButton.onClick.AddListener(OnForgotPasswordClicked);
        signUpButton.onClick.AddListener(OnSignUpClicked);
        googleLoginButton.onClick.AddListener(OnGoogleLoginClicked);
    }

    private void OnLoginClicked()
    {
        string email = emailInput.text.Trim();
        string password = passwordInput.text.Trim();

        // Basic validation
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            Debug.LogWarning("Please enter both Email and Password.");
            return;
        }

        // Send to backend
        StartCoroutine(LoginCoroutine(email, password));
    }

    private void OnForgotPasswordClicked()
    {
        SceneManager.LoadScene("ForgotPassword");
    }

    private void OnSignUpClicked()
    {
        SceneManager.LoadScene("acc creation");
    }

    private void OnGoogleLoginClicked()
    {
        Debug.Log("Google Login clicked! Integrate your Google login logic here.");
    }

    // -----------------------------
    // Login Coroutine
    // -----------------------------
    private IEnumerator LoginCoroutine(string email, string password)
    {
        // Create a JSON payload
        LoginRequest requestData = new LoginRequest { email = email, password = password };
        string jsonData = JsonUtility.ToJson(requestData);

        // Prepare the UnityWebRequest
        UnityWebRequest request = new UnityWebRequest(loginEndpoint, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Send request
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Login Error: {request.error}\nResponse: {request.downloadHandler.text}");
        }
        else
        {
            // If login is successful, the backend returns a JWT in the response body
            string token = request.downloadHandler.text;
            Debug.Log($"Login successful! JWT token received: {token}");

            // Store the JWT in PlayerPrefs (or a static variable, or both)
            PlayerPrefs.SetString("AuthToken", token);
            Debug.Log($"JWT Token saved {token}");
            PlayerPrefs.Save();

            // Optionally store in a static variable for quick access:
            // GlobalAuth.Token = token;

            // Now load your home scene
            SceneManager.LoadScene("Home");
        }
    }
}

[System.Serializable]
public class LoginRequest
{
    public string email;
    public string password;
}
