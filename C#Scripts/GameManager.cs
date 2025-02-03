using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour{
    [Header("Game Settings")]
    public float levelTime = 60f;
    public int totalApples = 10;
    private int collectedApples = 0;
    private float currentTime;
    private bool isGameActive = false;
    private int currentLevel = 1;

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI appleCountText;
    public TextMeshProUGUI instructionText;
    public GameObject levelCompletePanel;
    public Image[] starImages;
    public Button replayButton;
    public Button nextLevelButton;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] numberSounds; // Add audio clips for numbers 1-10

    [Header("Star Thresholds")]
    public float threeStarTime = 30f;
    public float twoStarTime = 45f;

    void Start(){
        InitializeGame();
    }

    void InitializeGame(){
        currentTime = levelTime;
        collectedApples = 0;
        isGameActive = true;
        levelCompletePanel.SetActive(false);
       
        // Set instruction text
        instructionText.text = "Touch the apples on the trees to make them fall!\nCollect all apples before time runs out!";

        UpdateUI();
        StartCoroutine(TimerCoroutine());
    }

    void UpdateUI(){
        timerText.text = $"Time: {Mathf.CeilToInt(currentTime)}s";
        appleCountText.text = $"Apples: {collectedApples}/{totalApples}";
    }

    IEnumerator TimerCoroutine(){
        while (currentTime > 0 && isGameActive){
            yield return new WaitForSeconds(1f);
            currentTime--;
            UpdateUI();
            if (currentTime <= 0){
                EndGame();
            }
        }
    }

    public void CollectApple(){
        collectedApples++;
               
        // Play number sound
        if (audioSource != null && numberSounds != null && collectedApples - 1 < numberSounds.Length){
            audioSource.clip = numberSounds[collectedApples - 1];
            audioSource.Play();
        }
        
        UpdateUI();

        if(collectedApples >= totalApples){
            EndGame();
        }
    }

    void EndGame(){
        isGameActive = false;
        levelCompletePanel.SetActive(true);
        instructionText.gameObject.SetActive(false);
        int stars = CalculateStars();
        DisplayStars(stars);
    }

    int CalculateStars(){
        if(collectedApples < totalApples){
            return 1;
        } 
        if(currentTime >= threeStarTime){
            return 3;
        } 
        if(currentTime >= twoStarTime){
            return 2;
        }
        return 1;
    }

    void DisplayStars(int stars){
        for(int i = 0; i < starImages.Length; i++){
            starImages[i].enabled = i < stars;
        }
    }

    public void ReplayLevel(){
        instructionText.gameObject.SetActive(true);
        InitializeGame();
    }

    public void NextLevel(){
        currentLevel++;
        Debug.Log("Loading next level...");
        InitializeGame(); // Reset the game for the next level
    }
}