using UnityEngine;
using UnityEngine.SceneManagement;


public class NewScriptableObjectScript : MonoBehaviour
{
    public string[] scenes;

    public int prevGameScore;

    public void LoadNextScene(string activeScene)
    {
        switch (activeScene)
        {
            case "game1":
                SceneManager.LoadScene($"game2{GameModeSelection("game2")}");
                break;

            case "game2":
                SceneManager.LoadScene($"game2{GameModeSelection("game2")}");
                break;

            case "game3":
                SceneManager.LoadScene($"game2{GameModeSelection("game3")}");
                break;

            case "game4":
                SceneManager.LoadScene($"game2{GameModeSelection("game4")}");
                break;

            case "game5":
                SceneManager.LoadScene($"game2{GameModeSelection("game5")}");
                break;

            case "game6":
                SceneManager.LoadScene("Home");
                break;
        }
    }

    private string GameModeSelection(string sceneName)
    {
        if (prevGameScore > 8000)
        {
            return "-Hard";
        }
        else
        {
            return "";
        }
    }
}
