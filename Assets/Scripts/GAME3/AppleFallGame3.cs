using UnityEngine;
using System.Collections.Generic;
using TMPro; // Import TextMeshPro for UI text display

public class AppleFallGame3 : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasFallen = false;
    private Vector2 initialPosition;

    public static List<AppleFallGame3> allApples = new List<AppleFallGame3>();

    public static int redAppleClickedCount = 0;
    public static int greenAppleClickedCount = 0;

    public string appleColor; // "Red" or "Green"

    public static int requiredRedApples;
    public static int requiredGreenApples;

    // Reference to the Dialog script
    public Dialog dialog;

    // Sound effects for red and green apples
    public AudioClip redAppleSound;
    public AudioClip greenAppleSound;
    private AudioSource audioSource;

    // UI text element for displaying apple requirements
    private static TMP_Text requirementText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        initialPosition = transform.position;
        allApples.Add(this);

        // Initialize AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the apple!");
        }

        // Find the Dialog script if not assigned
        if (dialog == null)
        {
            dialog = FindObjectOfType<Dialog>();
            if (dialog == null)
            {
                Debug.LogError("Dialog script not found in the scene!");
            }
        }

        // Find and assign the UI text element for displaying instructions
        if (requirementText == null)
        {
            requirementText = GameObject.Find("RequirementText")?.GetComponent<TMP_Text>();
            if (requirementText == null)
            {
                Debug.LogError("RequirementText UI element not found in the scene!");
            }
        }

        // Start a new round only once at the beginning
        if (allApples.Count == 1)
        {
            StartNewRound();
        }
    }

    void OnDestroy()
    {
        allApples.Remove(this);
    }

    void OnMouseDown()
    {
        if (!hasFallen)
        {
            rb.gravityScale = 1;
            hasFallen = true;

            // Update counters based on apple color
            if (appleColor == "Red")
            {
                redAppleClickedCount++;
                PlaySound(redAppleSound); // Play sound for red apple
            }
            else if (appleColor == "Green")
            {
                greenAppleClickedCount++;
                PlaySound(greenAppleSound); // Play sound for green apple
            }

            // Print the updated counters
            Debug.Log($"Clicked Red Apples: {redAppleClickedCount}, Clicked Green Apples: {greenAppleClickedCount}");

            // Check if player has collected the required apples (but don't end the game yet)
            CheckApplesCollected();

            // Ignore collisions with other apples
            Collider2D myCollider = GetComponent<Collider2D>();
            foreach (AppleFallGame3 apple in allApples)
            {
                if (apple != this) // Ignore itself
                {
                    Collider2D otherCollider = apple.GetComponent<Collider2D>();
                    Physics2D.IgnoreCollision(myCollider, otherCollider, true); // Ignore collision between apples
                }
            }
        }
    }

    public void ResetApple()
    {
        transform.position = initialPosition;
        rb.gravityScale = 0;
        rb.linearVelocity = Vector2.zero;
        hasFallen = false;
    }

    // Reset counts when replay is clicked
    public static void ResetCounts()
    {
        redAppleClickedCount = 0;
        greenAppleClickedCount = 0;
        Debug.Log($"Game Reset: Clicked Red Apples: {redAppleClickedCount}, Clicked Green Apples: {greenAppleClickedCount}");
    }

    // Start a new round with new random numbers
    public static void StartNewRound()
    {
        // Reset apple counts for a new round
        ResetCounts();

        // Generate new requirements
        requiredRedApples = Random.Range(1, 9);  // 1 to 8 red apples
        requiredGreenApples = Random.Range(1, 8); // 1 to 7 green apples

        // Update UI text with the new requirements
        string instructionText = $"Pick {requiredRedApples} red apples and {requiredGreenApples} green apples.";
        if (requirementText != null)
        {
            requirementText.text = instructionText;
        }
        else
        {
            Debug.LogWarning("RequirementText UI element is missing!");
        }

        // Display message in the console
        Debug.Log(instructionText);

        // Show a dialog box with new instructions
        Dialog dialog = FindObjectOfType<Dialog>();
        if (dialog != null)
        {
            dialog.RunEvent(2, () =>
            {
                Debug.Log("New round instructions shown.");
            });
        }
    }

    // Method to check if player collected correct apples (does NOT end the game automatically)
    public static bool CheckApplesCollected()
    {
        if (redAppleClickedCount >= requiredRedApples && greenAppleClickedCount >= requiredGreenApples)
        {
            Debug.Log("Player has collected the required number of apples!");
            return true;
        }
        return false;
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