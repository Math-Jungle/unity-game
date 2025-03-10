using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestButtonScript : MonoBehaviour
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

    public void Click()
    {
        Debug.Log("SignUp button Clicked");
        SceneManager.LoadScene("Game 1");
    }
}
