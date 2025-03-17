using UnityEngine;
using System.Collections.Generic;

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
    public AudioClip appleSelectSound; // Sound when the apple is clicked
    public AudioClip appleReleasedSound; // Sound when the apple is released
    public AudioClip appleHitSound; // Sound when the apple hits the ground
    private AudioSource audioSource;

    private bool hasHitGround = false; // Track if the apple has hit the ground

    // Flag to check if the question has been shown already
    private static bool questionDisplayed = false;

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

        // Only display the question once at the beginning
        if (!questionDisplayed)
        {
            StartNewRound();
            questionDisplayed = true;

            // Trigger the dialog for the game instructions
            if (dialog != null)
            {
                dialog.RunEvent(0, () =>
                {
                    Debug.Log("Dialog completed, starting the game.");
                });
            }
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

            // Play sound when the apple is clicked
            PlaySound(appleSelectSound);

            // Print the updated counters
            Debug.Log($"Clicked Red Apples: {redAppleClickedCount}, Clicked Green Apples: {greenAppleClickedCount}");

            // Check if the player has collected the required number of apples
            if (redAppleClickedCount >= requiredRedApples && greenAppleClickedCount >= requiredGreenApples)
            {
                Debug.Log("Player has collected the required number of apples!");

                // Trigger the dialog for completing the round
                if (dialog != null)
                {
                    dialog.RunEvent(1, () =>
                    {
                        Debug.Log("Round completed dialog shown.");
                        // Optionally, start a new round or end the game
                        StartNewRound();
                    });
                }
            }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Play sound when the apple hits the ground
        if (!hasHitGround && collision.gameObject.CompareTag("Ground"))
        {
            PlaySound(appleHitSound);
            hasHitGround = true; // Ensure the sound plays only once
        }
    }

    public void ResetApple()
    {
        transform.position = initialPosition;
        rb.gravityScale = 0;
        rb.linearVelocity = Vector2.zero;
        hasFallen = false;
        hasHitGround = false; // Reset ground hit flag
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
        // Randomly generate numbers of red and green apples to pick
        requiredRedApples = Random.Range(1, 9);  // 1 to 8 red apples
        requiredGreenApples = Random.Range(1, 8); // 1 to 7 green apples

        // Display the new question in the console
        Debug.Log($"Pick {requiredRedApples} red apples and {requiredGreenApples} green apples.");

        // Optionally, trigger a dialog to show the new requirements
        Dialog dialog = FindObjectOfType<Dialog>();
        if (dialog != null)
        {
            dialog.RunEvent(2, () =>
            {
                Debug.Log("New round instructions shown.");
            });
        }
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