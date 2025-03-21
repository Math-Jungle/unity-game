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

    [SerializeField] private TMP_Text errorText;

    [Header("Backend URL")]
    [SerializeField] private string loginEndpoint = "https://spring-app-249115746984.asia-south1.run.app/user/login";
    [SerializeField] private string userDetailsEndpoint = "https://your-backend-url/user/details";

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

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            Debug.LogWarning("Please enter both Email and Password.");
            if (errorText != null) errorText.text = "Please enter both Email and Password.";
            return;
        }

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

    private IEnumerator LoginCoroutine(string email, string password)
    {
        LoginRequest requestData = new LoginRequest { email = email, password = password };
        string jsonData = JsonUtility.ToJson(requestData);

        UnityWebRequest request = new UnityWebRequest(loginEndpoint, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (errorText != null)
            errorText.text = ""; // Clear old errors

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Login Error: {request.error}\nResponse: {request.downloadHandler.text}");
            if (request.responseCode == 401)
            {
                if (errorText != null)
                    errorText.text = "Invalid email or password. Please try again.";
            }
            else
            {
                if (errorText != null)
                    errorText.text = $"Error {request.responseCode}: {request.downloadHandler.text}";
            }
        }
        else
        {
            // 200 OK
            string token = request.downloadHandler.text;
            Debug.Log($"Login successful! JWT token received: {token}");

            // 1) Store JWT token
            PlayerPrefs.SetString("AuthToken", token);
            PlayerPrefs.Save();

            // 2) Immediately fetch user data with that token
            yield return StartCoroutine(FetchUserDataCoroutine(token));

            // 3) Load Home scene
            SceneManager.LoadScene("Home");
        }
    }

    // NEW: fetch user details (child name, avatarId, etc.) from the backend
    private IEnumerator FetchUserDataCoroutine(string token)
    {
        UnityWebRequest request = UnityWebRequest.Get(userDetailsEndpoint);
        request.SetRequestHeader("Authorization", "Bearer " + token);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            Debug.Log("Fetched user data JSON: " + json);

            try
            {
                UserData userData = JsonUtility.FromJson<UserData>(json);
                if (userData != null)
                {
                    Debug.Log($"User data: childName={userData.childName}, avatarId={userData.avatarId}");

                    // 2a) Assign it to the GameManager so the Home scene can display it
                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.CurrentUserData = userData;
                        Debug.Log("Assigned user data to GameManager.");
                    }
                    else
                    {
                        Debug.LogWarning("No GameManager instance found to store user data!");
                    }
                }
                else
                {
                    Debug.LogWarning("Failed to parse user data from JSON.");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error parsing user data: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("Failed to fetch user data: " + request.error);
        }
    }
}

[System.Serializable]
public class LoginRequest
{
    public string email;
    public string password;
}
