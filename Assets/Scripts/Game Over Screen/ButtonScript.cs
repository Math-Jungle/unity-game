// using System.Collections;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class ButtonScript : MonoBehaviour
// {
//     public string NextSceneName { get; set; } = "Home";
//     public int gameScore;
//     public LoadingScenes loadingScenes;
//     public GameOverScreen gameOverScreen;
//     public GameObject gameOverScreenObject;

//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {

//     }

//     // Update is called once per frame
//     void Update()
//     {

//     }

//     // IEnumerator LoadNextScene()
//     // {
//     //     //gameScore = LManager.Instance.CurrentScore;

//     //     NextSceneName = (gameScore > 8000) ? $"{NextSceneName}-Hard" : NextSceneName;

//     //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(NextSceneName);
//     //     while (!asyncLoad.isDone)
//     //     {
//     //         Debug.Log("Loading progress: " + (asyncLoad.progress * 100) + "%");
//     //         yield return null;
//     //     }
//     // }

//     public void NextLevel(string nextSceneName)
//     {
//         Time.timeScale = 1f;
//         SceneManager.LoadScene(nextSceneName);
//     }

//     public void SceneLoader()
//     {
//         gameOverScreenObject.SetActive(false);
//         Time.timeScale = 1f;
//         loadingScenes.LoadNextScene(SceneManager.GetActiveScene().name, gameOverScreen.score);
//     }

//     public void RestartLevel()
//     {
//         Time.timeScale = 1f;
//         string currentSceneName = SceneManager.GetActiveScene().name;
//         SceneManager.LoadScene(currentSceneName);
//     }

//     public void Home()
//     {
//         Time.timeScale = 1f;
//         SceneManager.LoadScene("Home");
//     }
// }
// // Move all this code to the gameover screen script