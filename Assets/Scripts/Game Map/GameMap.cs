using UnityEngine;

public class GameMap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Game1()
    {
        Debug.Log("Game 1 button Clicked");
        UIManager.instance.LoadScene("Game 1");
    }

    public void Game2()
    {
        Debug.Log("Game 2 button Clicked");
        UIManager.instance.LoadScene("Sequence Number Game");
    }

    public void Game3()
    {
        Debug.Log("Game 3 button Clicked");
        UIManager.instance.LoadScene("BasketGame");
    }

    public void Game4()
    {
        Debug.Log("Game 4 button Clicked");
        UIManager.instance.LoadScene("Game 3");
    }

    public void Game5()
    {
        Debug.Log("Game 5 button Clicked");
        UIManager.instance.LoadScene("ScaleGame");
    }

    public void Home()
    {
        Debug.Log("Home button Clicked");
        UIManager.instance.LoadScene("Home");
    }
}
