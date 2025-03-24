using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BasketScript : MonoBehaviour
{
    public int requiredApples; // Number of apples required for this basket
    private int currentApples = 0; // Current number of apples in the basket
    public TextMeshProUGUI appleCountText; // Reference to the TextMeshPro component
    public GameObject tryAgainButton; // Reference to the Try Again button
    private AppleScript[] apples; // Array to store all apples in the scene

    public Dialog dialog; // Reference to the Dialog script

    public AudioClip appleAddedToBasketSound; // Sound when an apple is added to the basket
    public AudioClip replaySound; // Sound when the replay button is clicked
    private AudioSource audioSource; // AudioSource for sound effects

    private float startTime; // Track the start time of the level

    private int calculatedScore = 0; // Store the calculated score
    private List<float> reactionTimes = new List<float>(); // Store reaction times
    private float lastAppleTime; // Store the last apple drop time

    private void Start()
    {
        if (dialog == null)
        {
            dialog = FindFirstObjectByType<Dialog>();
            if (dialog == null)
            {
                Debug.LogError("Dialog script not found in the scene!");
            }
        }

        if (dialog != null)
        {
            // Subscribe to the dialog complete event
            dialog.OnDialogComplete += StartGameTimer;
            dialog.RunEvent(0);
            Debug.Log("Dialog started");
        }
        else
        {
            Debug.LogError("dialogBox is not assigned!");
        }

        // Initialize the UI text
        UpdateAppleCountText();

        // Find all apples in the scene
        apples = FindObjectsOfType<AppleScript>();

        // Hide the Try Again button initially
        if (tryAgainButton != null)
        {
            tryAgainButton.SetActive(false);
        }

        // Initialize AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the basket!");
        }

        // Record the start time of the level
        startTime = Time.time;
        lastAppleTime = startTime; // Initialize the last apple time
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Apple"))
        {
            float currentTime = Time.time;
            reactionTimes.Add(currentTime - lastAppleTime); // Store reaction time
            lastAppleTime = currentTime;

            currentApples++;
            UpdateAppleCountText(); // Update the UI text

            // Disable the apple so it doesn't interact further
            collision.gameObject.SetActive(false);

            // Play sound when an apple is added to the basket
            PlaySound(appleAddedToBasketSound);

            //Calculating the score with each iteration

            Debug.Log("Basket has the required number of apples!");
            calculatedScore = CalculateScore(); // Calculate the score


            // EndGame() will only be called when the "Next" button is clicked

        }
    }

    private void UpdateAppleCountText()
    {
        if (appleCountText != null)
        {
            appleCountText.text = "Bananas: " + currentApples;
        }
    }

    public void EndGame()
    {
        float gameDuration = Time.time - startTime; // Calculate game duration
        GameManager.Instance.SubmitGameResults(calculatedScore, reactionTimes, gameDuration);
    }

    public void RestoreApples()
    {
        currentApples = 0; // Reset the apple count
        UpdateAppleCountText(); // Update the UI text

        // Restore all apples to their initial positions
        foreach (AppleScript apple in apples)
        {
            apple.ResetApple();
        }

        // Hide the Try Again button
        if (tryAgainButton != null)
        {
            tryAgainButton.SetActive(false);
        }
    }

    public void OnReplayButtonClicked()
    {
        PlaySound(replaySound);
        RestoreApples();
    }

    public int CalculateScore()
    {
        requiredApples = 7; // Change this to the required number of apples displayed on the basket
        int baseScore = 0;
        int timeBonus = 0;

        // Calculate the difference between currentApples and requiredApples
        int appleDifference = Mathf.Abs(currentApples - requiredApples);

        // Assign baseScore based on the number of apples placed
        if (currentApples == requiredApples) // Perfect score range (3 stars)
        {
            baseScore = 7000;
        }
        else if (currentApples == requiredApples - 1 || currentApples == requiredApples + 1) // Slightly off (2 stars)
        {
            baseScore = 5000;
        }
        else if (currentApples == requiredApples - 2 || currentApples == requiredApples + 2) // Further off (1 star)
        {
            baseScore = 3000;
        }
        else // Out of range (no stars or very low score)
        {
            baseScore = Mathf.Max(1000, 3000 - (appleDifference * 1000)); // Minimum 1000
        }

        // Calculate the time bonus only if apples were added
        if (currentApples > 0)
        {
            timeBonus = CalculateTimeBonus();
        }

        // Ensure final score stays within the intended ranges
        int finalScore = baseScore + timeBonus;

        if (currentApples == requiredApples)
        {
            finalScore = Mathf.Clamp(finalScore, 7000, 10000);
        }
        else if (currentApples == requiredApples - 1 || currentApples == requiredApples + 1)
        {
            finalScore = Mathf.Clamp(finalScore, 5000, 7000);
        }
        else if (currentApples == requiredApples - 2 || currentApples == requiredApples + 2)
        {
            finalScore = Mathf.Clamp(finalScore, 3000, 5000);
        }
        else
        {
            finalScore = Mathf.Max(1000, finalScore); // Prevents scores below 1000 for extreme cases
        }

        // Debugging logs
        Debug.Log($"Current Apples: {currentApples}");
        Debug.Log($"Apple Difference: {appleDifference}");
        Debug.Log($"Base Score: {baseScore}");
        Debug.Log($"Time Bonus: {timeBonus}");
        Debug.Log($"Final Score: {finalScore}");

        return finalScore;
    }


    public int CalculateTimeBonus()
    {
        float timeTaken = Time.time - startTime;
        int maxTimeBonus = 1000;
        int minTimeBonus = 100;

        int timeBonus = Mathf.RoundToInt(Mathf.Lerp(maxTimeBonus, minTimeBonus, timeTaken / 60f));
        Debug.Log($"Time Bonus: {timeBonus}");
        return timeBonus;
    }

    private void StartGameTimer()
    {
        // Logic to start the game timer (if needed)
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing!");
        }
    }
}