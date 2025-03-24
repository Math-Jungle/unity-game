using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DashboardManager : MonoBehaviour
{

    private GameData gameData;
    [SerializeField] private TextMeshProUGUI gameTimeText;
    [SerializeField] private TextMeshProUGUI gameLevelText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance not found.");
            return;
        }
        gameData = GameManager.Instance.GetGameData();
        SetGameLevel();
        SetGameTime();

    }

    private void SetGameTime()
    {
        // Set the game time
        gameData.GetTotalGameTime();

        if (gameLevelText != null)
        {
            gameTimeText.text = (gameData.GetTotalGameTime() / 60f).ToString("F0") + "min"; // F0 means zero decimal places
        }
    }

    private void SetGameLevel()
    {
        // Set the game level
        if (gameLevelText != null)
        {
            gameLevelText.text = gameData.gameLevels.Count.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Home()
    {
        SceneManager.LoadScene("Home");
    }
}
