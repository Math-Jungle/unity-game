using UnityEngine;

public class AppleBehaviorScriptForGame2 : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    private static int applesTouched = 0; // Track the apples touched
    private static int applesToEndGameLevel = 6; // Number of apples needed to finish the game level
    public GameObject gameOverScreen; // Reference to the GameOver UI
    private static bool gameEnded = false; // Flag to prevent further interactions
    private Collider2D appleCollider;
    private bool isDragging = false;
    private Vector2 touchOffset;

    // Start is called before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0; // Prevent the apples from falling initially
        appleCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded) return; // Stop processing touches after game over

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                    if (hit.collider != null && hit.collider.gameObject == gameObject)
                    {
                        isDragging = true;
                        touchOffset = (Vector2)transform.position - touchPosition;
                        rigidbody2D.gravityScale = 0; // Disable gravity while dragging
                        rigidbody2D.linearVelocity = Vector2.zero; // Stop any existing movement
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        transform.position = touchPosition + touchOffset; // Move the apple with the touch
                    }
                    break;

                case TouchPhase.Ended:
                    if (isDragging)
                    {
                        isDragging = false;
                        rigidbody2D.gravityScale = 1; // Re-enable gravity when released
                        applesTouched++;
                        Debug.Log("Apple Touched Total: " + applesTouched);

                        if (applesTouched >= applesToEndGameLevel)
                        {
                            EndGame();
                        }
                    }
                    break;
            }
        }
    }

    void EndGame()
    {
        Debug.Log("Game Over! You touched 6 apples.");
        gameEnded = true; // Set flag to prevent further interactions

        // Disable all apple colliders in the scene
        AppleBehaviorScriptForGame2[] apples = FindObjectsOfType<AppleBehaviorScriptForGame2>();
        foreach (AppleBehaviorScriptForGame2 apple in apples)
        {
            Collider2D appleCol = apple.GetComponent<Collider2D>();
            if (appleCol != null)
            {
                appleCol.enabled = false; // Disable apple colliders
            }
        }
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true); // Show Game Over Screen
        }
    }
}