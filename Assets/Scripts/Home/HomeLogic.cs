using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeLogic : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameMap()
    {
        Debug.Log("SignUp button Clicked");
        SceneManager.LoadScene("game map");
    }
}
