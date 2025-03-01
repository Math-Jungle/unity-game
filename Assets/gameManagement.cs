using UnityEngine;

public class gameManagement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject gameWinUI;



    public void GameWin()
    {
        if (gameWinUI == null)
        {
            Debug.LogError("gameWinUI is not assigned!");
            return;
        }

        if (gameWinUI.activeInHierarchy == false)
        {
            gameWinUI.SetActive(true);
        }
        else
        {
            gameWinUI.SetActive(false);
        }
    }
}
