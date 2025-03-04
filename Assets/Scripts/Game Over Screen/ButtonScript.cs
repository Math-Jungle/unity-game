using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public string nextSceneName = "Home";
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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);
        while (!asyncLoad.isDone)
        {
            Debug.Log("Loading progress: " + (asyncLoad.progress * 100) + "%");
            yield return null;
        }
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        Debug.Log($"Next Scene before loading: {nextSceneName}");
        if (string.IsNullOrEmpty(nextSceneName))
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
