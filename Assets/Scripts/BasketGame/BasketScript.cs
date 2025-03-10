using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BasketScript : MonoBehaviour
{
    public int requiredApples; // Number of apples required for this basket
    private int currentApples = 0; // Current number of apples in the basket
    public TextMeshProUGUI appleCountText; // Reference to the TextMeshPro component
    public GameObject tryAgainButton; // Reference to the Try Again button
    private AppleScript[] apples; // Array to store all apples in the scene

    public gameManagement gameManager; // Reference to the gameManagement script
    public gameOverScreen gameOverScreen; // Reference to the gameOverScreen script
    public Dialog dialog; // Reference to the Dialog script

    public AudioClip appleAddedToBasketSound; // Sound when an apple is added to the basket
    public AudioClip replaySound; // Sound when the replay button is clicked
    private AudioSource audioSource; // AudioSource for sound effects

    private float startTime; // Track the start time of the level
    private bool levelCompleted = false; // Track if the level is completed
    private int calculatedScore = 0; // Store the calculated score

    private void Start()
    {
        if (dialog == null)
        {
            dialog = FindObjectOfType<Dialog>();
            if (dialog == null)
            {
                Debug.LogError("Dialog script not found in the scene!");
            }
        }

        if (dialog != null)
        {
            // Subscribe to the dialog complete event
            dialog.OnDialogComplete += StartGameTimer;
            dialog.RunEvent(1);
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

        // Find the gameManagement object in the scene
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<gameManagement>();
            if (gameManager == null)
            {
                Debug.LogError("gameManagement script not found in the scene!");
            }
        }

        // Find the gameOverScreen object in the scene
        if (gameOverScreen == null)
        {
            gameOverScreen = FindObjectOfType<gameOverScreen>();
            if (gameOverScreen == null)
            {
                Debug.LogError("gameOverScreen script not found in the scene!");
            }
        }

        // Initialize AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the basket!");
        }

        // Record the start time of the level
        startTime = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Apple"))
        {
            currentApples++;
            UpdateAppleCountText(); // Update the UI text

            // Disable the apple so it doesn't interact further
            collision.gameObject.SetActive(false);

            // Play sound when an apple is added to the basket
            PlaySound(appleAddedToBasketSound);

            // Check if the basket has the required number of apples
            if (currentApples >= requiredApples)
            {
                Debug.Log("Basket has the required number of apples!");
                levelCompleted = true; // Mark the level as completed
                calculatedScore = CalculateScore(); // Calculate the score
            }
        }
    }

    private void UpdateAppleCountText()
    {
        if (appleCountText != null)
        {
            appleCountText.text = "Bananas: " + currentApples;
        }
    }

    // Method to restore all apples to their initial positions
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

    // Method to handle the confirm button click
    public void OnConfirmButtonClicked()
    {
        if (levelCompleted)
        {
            // Show the scorecard popup
            if (gameManager != null)
            {
                gameManager.GameWin(); // Activate the scorecard popup
            }
            else
            {
                Debug.LogError("gameManager script is not assigned!");
            }

            // Pass the calculated score to the gameOverScreen script
            if (gameOverScreen != null)
            {
                Debug.Log($"Passing score to gameOverScreen: {calculatedScore}");
                gameOverScreen.SetScore(calculatedScore);
            }
            else
            {
                Debug.LogError("gameOverScreen script is not assigned!");
            }
        }
        else
        {
            // Show the Try Again button
            if (tryAgainButton != null)
            {
                tryAgainButton.SetActive(true);
            }
        }
    }

    // Method to handle the replay button click
    public void OnReplayButtonClicked()
    {
        // Play sound when the replay button is clicked
        PlaySound(replaySound);

        // Restore all apples to their initial positions
        RestoreApples();
    }

    // Method to calculate the score
    public int CalculateScore()
    {
        requiredApples = 7; // Change this to the required number of apples as on the text on the basket
        int baseScore = 0;
        int timeBonus = 0;

        // Calculate the difference between currentApples and requiredApples
        int appleDifference = Mathf.Abs(currentApples - requiredApples);

        // Calculate the base score based on the apple difference
        if (currentApples == requiredApples)
        {
            // Perfect score if the player places exactly the required number of apples
            baseScore = 7000; // Maximum base score for 3 stars
        }
        else if ((0 < currentApples && currentApples < 7) || (7 < currentApples && currentApples <= 12))
        {
            // Reduce the base score based on the gap between currentApples and requiredApples
            baseScore = Mathf.Max(0, 7000 - (appleDifference * 1000)); // Reduce base score by 1000 points per apple difference
        }

        // Calculate the time bonus only if the player places at least 5 apples
        if (currentApples >= 5 && currentApples <= 9)
        {
            timeBonus = CalculateTimeBonus();
        }

        // Total score is the sum of base score and time bonus
        int totalScore = baseScore + timeBonus;

        Debug.Log($"Current Apples: {currentApples}");
        Debug.Log($"Apple Difference: {appleDifference}");
        Debug.Log($"Base Score: {baseScore}");
        Debug.Log($"Time Bonus: {timeBonus}");
        Debug.Log($"Total Score: {totalScore}");

        return totalScore;
    }

    // Method to calculate the time bonus
    public int CalculateTimeBonus()
    {
        float timeTaken = Time.time - startTime; // Time taken to complete the level
        int maxTimeBonus = 1000; // Maximum time bonus
        int minTimeBonus = 100; // Minimum time bonus

        // Time bonus decreases linearly with time taken
        int timeBonus = Mathf.RoundToInt(Mathf.Lerp(maxTimeBonus, minTimeBonus, timeTaken / 60f)); // Adjust 60f based on your level duration
        Debug.Log($"Time Bonus: {timeBonus}");
        return timeBonus;
    }

    private void StartGameTimer()
    {
        // Logic to start the game timer (if needed)
    }

    // Method to play a sound
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