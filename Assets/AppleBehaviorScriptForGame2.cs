using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class AppleBehaviorScriptForGame2 : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    private static int applesTouched = 0; // Track the apples touched
    private static int applesToEndGameLevel = 6; // Number of apples need to be touched to finish the game level
    public GameObject gameOverScreen; // Reference to the GameOver UI
    private static bool gameEnded = false; // Flag to prevent further interactions
    private Collider2D appleCollider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    rigidbody2D.gravityScale = 1;
                    applesTouched++;
                    Debug.Log("Apple Touched Total: " + applesTouched);

                    if (applesTouched >= applesToEndGameLevel){
                        EndGame();
                    }
                }
            }
        }
    }

    void EndGame()
    {
        Debug.Log("Game Over! You touched 6 apples.");
        gameEnded = true; // Set flag to prevent further interactions

        // Disable all apple colliders in the scene
        AppleBehaviourScript[] apples = FindObjectsOfType<AppleBehaviourScript>();
        foreach (AppleBehaviourScript apple in apples)
        {
            Collider2D appleCol = apple.GetComponent<Collider2D>();
            if (appleCol != null){
                appleCol.enabled = false; // Disable apple colliders
            }
        }
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true); // Show Game Over Screen
        }
    }
}
