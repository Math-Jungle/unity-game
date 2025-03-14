// using System;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;

// public class LoadingScenes : MonoBehaviour
// {
//     public GameObject LoadingScreen;
//     public Image LoadingBarFill;
//     private string mode;

//     // Use a library to store the game scenes 

//     // public void LoadScene(int sceneId)
//     // {
//     //     StartCoroutine(LoadSceneAsync(sceneId));
//     // }

//     void Start()
//     {
//         if (LoadingScreen != null)
//         {
//             LoadingScreen.SetActive(false);
//         }
//     }

//     IEnumerator LoadSceneAsync(string sceneId)
//     {
//         AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

//         LoadingScreen.SetActive(true);

//         while (!operation.isDone)
//         {
//             float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

//             if (LoadingBarFill != null)
//             {
//                 LoadingBarFill.fillAmount = progressValue;
//             }
//             else
//             {
//                 Debug.LogError("LoadingBarFill is not assigned.");
//             }

//             yield return null;
//         }

//         LoadingScreen.SetActive(false);
//     }

//     public void LoadNextGame(string activeScene, int prevGameScore = 0)
//     {
//         if (prevGameScore > 8000)
//         {
//             // mode = "-Hard";
//             mode = "";
//         }
//         else
//         {
//             mode = "";
//         }

//         switch (activeScene)
//         {
//             case "Game 1":
//                 StartCoroutine(LoadSceneAsync($"Sequence Number Game{mode}"));
//                 break;

//             case "Sequence Number Game":
//                 StartCoroutine(LoadSceneAsync($"game3{mode}"));
//                 break;

//             case "game3":
//                 StartCoroutine(LoadSceneAsync($"game4{mode}"));
//                 break;

//             case "game4":
//                 StartCoroutine(LoadSceneAsync($"game5{mode}"));
//                 break;

//             case "game5":
//                 StartCoroutine(LoadSceneAsync($"game6{mode}"));
//                 break;

//             case "game6":
//                 StartCoroutine(LoadSceneAsync("Home"));
//                 break;

//             default:
//                 StartCoroutine(LoadSceneAsync("Home"));
//                 break;
//         }
//     }

//     public void LoadScene(string sceneName)
//     {
//         StartCoroutine(LoadSceneAsync(sceneName));
//     }



//     // private string GameModeSelection(int prevGameScore)
//     // {
//     //     if (prevGameScore > 8000)
//     //     {
//     //         return "-Hard";
//     //     }
//     //     else
//     //     {
//     //         return "";
//     //     }
//     // }
// }
