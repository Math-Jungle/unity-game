using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic; // Add this line to use List<>

public class NextButtonManager : MonoBehaviour
{
    public Button nextButton;  // The button to trigger the check
    public AppleFallGame3 appleGame;  // Reference to the AppleFallGame3 script
    public Dialog dialog;  // Reference to the Dialog script
    public TextMeshProUGUI resultText;  // UI text to display the result message
    public GameObject gameOverScreen;  // Reference to the game over screen

    private float startTime; // Track the start time of the level
    private List<float> reactionTimes = new List<float>(); // Store reaction times
    private float lastAppleTime; // Store the last apple drop time

    void Start()
    {
        // Record the start time of the level
        startTime = Time.time;
        lastAppleTime = startTime;

        // Add listener to the button to call the method when clicked
        nextButton.onClick.AddListener(CheckAndDisplayMessage);
    }

    void CheckAndDisplayMessage()
    {
        // Calculate the score
        int calculatedScore = CalculateScore();

        // Display the game over screen
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        // Display the result message
        if (resultText != null)
        {
            resultText.text = $"Your score: {calculatedScore}";
        }

        // Submit game results to the GameManager
        float gameDuration = Time.time - startTime;
        GameManager.Instance.SubmitGameResults(calculatedScore, reactionTimes, gameDuration);
    }

    public int CalculateScore()
    {
        int baseScore = 0;
        int timeBonus = 0;

        // Calculate the difference between collected and required apples
        int redAppleDifference = Mathf.Abs(AppleFallGame3.redAppleClickedCount - AppleFallGame3.requiredRedApples);
        int greenAppleDifference = Mathf.Abs(AppleFallGame3.greenAppleClickedCount - AppleFallGame3.requiredGreenApples);

        // Assign baseScore based on the number of apples placed
        if (redAppleDifference == 0 && greenAppleDifference == 0) // Perfect score range (3 stars)
        {
            baseScore = 7000;
        }
        else if (redAppleDifference <= 1 && greenAppleDifference <= 1) // Slightly off (2 stars)
        {
            baseScore = 5000;
        }
        else if (redAppleDifference <= 2 && greenAppleDifference <= 2) // Further off (1 star)
        {
            baseScore = 3000;
        }
        else // Out of range (no stars or very low score)
        {
            baseScore = Mathf.Max(1000, 3000 - ((redAppleDifference + greenAppleDifference) * 1000)); // Minimum 1000
        }

        // Calculate the time bonus only if apples were added
        if (AppleFallGame3.redAppleClickedCount > 0 || AppleFallGame3.greenAppleClickedCount > 0)
        {
            timeBonus = CalculateTimeBonus();
        }

        // Ensure final score stays within the intended ranges
        int finalScore = baseScore + timeBonus;

        // Debugging logs
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
}