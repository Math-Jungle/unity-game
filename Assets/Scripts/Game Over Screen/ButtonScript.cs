using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public string NextSceneName { get; set; } = "Home";
    public int gameScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LoadNextScene()
    {
        gameScore = GamesManager.Instance.CurrentScore;

        NextSceneName = (gameScore > 8000) ? $"{NextSceneName}-Hard" : NextSceneName;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(NextSceneName);
        while (!asyncLoad.isDone)
        {
            Debug.Log("Loading progress: " + (asyncLoad.progress * 100) + "%");
            yield return null;
        }
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        Debug.Log($"Next Scene before loading: {NextSceneName}");
        if (string.IsNullOrEmpty(NextSceneName))
        {
            Debug.LogError("Next Scene Name is not set!");
            return; // Prevents loading an empty scene
        }

        StartCoroutine(LoadNextScene());
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Home");
    }
}
