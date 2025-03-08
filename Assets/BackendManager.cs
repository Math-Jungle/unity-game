using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class BackendManager : MonoBehaviour
{
    public static BackendManager instance; // Singleton pattern. only one object presist through the game
    private string backendUrl = "";
    private string backendDataUrl = "";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //Keeping this GameObject across all scenes
        }
        else
        {
            Destroy(gameObject); //Prevent duplicate instances
        }
    }

    void Start()
    {
        CheckUserLoginStatus();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            Debug.Log("Internet connected, syncing user data.");
            SyncUserData();
        }
    }


    private void CheckUserLoginStatus()
    {
        string jwtToken = PlayerPrefs.GetString("UserToken", "");

        if (string.IsNullOrEmpty(jwtToken))
        {
            Debug.Log("User not logged in. Loading Splash Screen.");
            SceneManager.LoadScene("SplashScreen");
        }
        else
        {
            Debug.Log("User is logged in. Verifying user.");
            StartCoroutine(VerifyUser(jwtToken));
        }
    }

    private IEnumerator VerifyUser(string jwtToken)
    {
        UnityWebRequest request = UnityWebRequest.Get(backendUrl + "?jwtToken=" + jwtToken);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success && request.downloadHandler.text == "valid")
        {
            Debug.Log("User Verified");
            SceneManager.LoadScene("Home");
        }
        else
        {
            Debug.Log("JWT Token Error");
            SceneManager.LoadScene("Splash");
        }

    }

    private void SyncUserData()
    {
        Debug.Log("Updating user progress.");
    }

    public void SendGameData(string gameLevel, int score, float[] reactionTimes, float gameTime)
    {
        // Creating GameData Object
        GameData data = new GameData(gameLevel, score, reactionTimes, gameTime);

        // Convert the GameData Object to Json
        string jsonData = JsonUtility.ToJson(data);

        //Starting coroutine to send data
        StartCoroutine(SendDataCoroutine(jsonData));

    }

    private IEnumerator SendDataCoroutine(string jsonData)
    {

        string jwtToken = PlayerPrefs.GetString("UserToken", "");

        if (string.IsNullOrEmpty(jwtToken))
        {
            Debug.LogError("No JWT Token found! Cannot send game data.");
            yield break;
        }

        UnityWebRequest request = new UnityWebRequest(backendDataUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer(); // Need to store recieved response
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + jwtToken);

        yield return request.SendWebRequest(); // Waiting for response

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Game data sent successfully: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error sending game data: " + request.error);
        }

    }
}

[System.Serializable]
public class GameData
{
    public string gameLevel;
    public int score;
    public float[] reactionTimes;
    public float gameTime;
    public string timestamp;

    public GameData(string gameLevel, int score, float[] reactionTimes, float gameTime)
    {
        this.gameLevel = gameLevel;
        this.score = score;
        this.reactionTimes = reactionTimes;
        this.gameTime = gameTime;
        this.timestamp = System.DateTime.UtcNow.ToString("o");  // Use ISO8601 format

    }

}
