// using System.Collections;
// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.Networking;
// using UnityEngine.SceneManagement;

// public class BackendManager : MonoBehaviour
// {
//     public static BackendManager instance; // Singleton pattern. only one object presist through the game
//     private string backendUrl = "";
//     private string backendDataUrl = "";

//     void Awake()
//     {
//         if (instance == null)
//         {
//             instance = this;
//             DontDestroyOnLoad(gameObject); //Keeping this GameObject across all scenes
//         }
//         else
//         {
//             Destroy(gameObject); //Prevent duplicate instances
//         }
//     }

//     void Start()
//     {
//         CheckUserLoginStatus();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (Application.internetReachability != NetworkReachability.NotReachable)
//         {
//             Debug.Log("Internet connected, syncing user data.");
//             SyncUserData();
//         }
//     }


//     private void CheckUserLoginStatus()
//     {
//         string jwtToken = PlayerPrefs.GetString("UserToken", "");

//         if (string.IsNullOrEmpty(jwtToken))
//         {
//             Debug.Log("User not logged in. Loading Splash Screen.");
//             SceneManager.LoadScene("SplashScreen");
//         }
//         else
//         {
//             Debug.Log("User is logged in. Verifying user.");
//             StartCoroutine(VerifyUser(jwtToken));
//         }
//     }

//     private IEnumerator VerifyToken(string jwtToken)
//     {
//         UnityWebRequest request = UnityWebRequest.Get(backendUrl + "?jwtToken=" + jwtToken);
//         yield return request.SendWebRequest();

//         if (request.result == UnityWebRequest.Result.Success && request.downloadHandler.text == "valid")
//         {
//             Debug.Log("User Verified");
//             SceneManager.LoadScene("Home");
//         }
//         else
//         {
//             Debug.Log("JWT Token Error");
//             SceneManager.LoadScene("Splash");
//         }

//     }

//     private void SyncUserData()
//     {
//         Debug.Log("Updating user progress.");
//     }

//     // public void SendGameData(string gameLevel, int score, float[] reactionTimes, float gameTime)
//     // {
//     //     // Creating GameData Object
//     //     GameLevel gameLevelData = new GameLevel(gameLevel, score, reactionTimes, gameTime);

//     //     // Convert the GameData Object to Json
//     //     string jsonData = JsonUtility.ToJson(gameLevelData);

//     //     //Starting coroutine to send data
//     //     StartCoroutine(SendDataCoroutine(jsonData));

//     // }


// }


