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

    // Flag to check if the question has been shown already
    private static bool questionDisplayed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        initialPosition = transform.position;
        allApples.Add(this);

        // Only display the question once at the beginning
        if (!questionDisplayed)
        {
            StartNewRound();
            questionDisplayed = true;
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
                redAppleClickedCount++;
            else if (appleColor == "Green")
                greenAppleClickedCount++;

            // Print the updated counters
            Debug.Log($"Clicked Red Apples: {redAppleClickedCount}, Clicked Green Apples: {greenAppleClickedCount}");

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
        // Randomly generate numbers of red and green apples to pick
        requiredRedApples = Random.Range(1, 9);  // 1 to 8 red apples
        requiredGreenApples = Random.Range(1, 8); // 1 to 7 green apples

        // Display the new question in the console
        Debug.Log($"Pick {requiredRedApples} red apples and {requiredGreenApples} green apples.");
    }
}
