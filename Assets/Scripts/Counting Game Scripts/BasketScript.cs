using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Required for scene management

public class BasketScript : MonoBehaviour
{
    public int requiredApples; // Number of apples required for this basket
    private int currentApples = 0; // Current number of apples in the basket
    public TextMeshProUGUI appleCountText; // Reference to the TextMeshPro component
    public GameObject tryAgainButton; // Reference to the Try Again button
    private AppleBehaviourScript[] apples; // Array to store all apples in the scene

    private void Start()
    {
        // Initialize the UI text
        UpdateAppleCountText();

        // Find all apples in the scene
        apples = FindObjectsOfType<AppleBehaviourScript>();

        // Hide the Try Again button initially
        if (tryAgainButton != null)
        {
            tryAgainButton.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Apple"))
        {
            currentApples++;
            UpdateAppleCountText(); // Update the UI text

            // Disable the apple so it doesn't interact further
            collision.gameObject.SetActive(false);

            // Check if the basket has the required number of apples
            if (currentApples >= requiredApples)
            {
                Debug.Log("Basket has the required number of apples!");
            }
        }
    }

    private void UpdateAppleCountText()
    {
        if (appleCountText != null)
        {
            appleCountText.text = "Apples: " + currentApples;
        }
    }

    // Method to restore all apples to their initial positions
    public void RestoreApples()
    {
        currentApples = 0; // Reset the apple count
        UpdateAppleCountText(); // Update the UI text

        // Restore all apples to their initial positions
        foreach (AppleBehaviourScript apple in apples)
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
        if (currentApples == requiredApples)
        {
            // Redirect to the "complete" scene
            SceneManager.LoadScene("complete");
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
}