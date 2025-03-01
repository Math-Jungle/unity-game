using UnityEngine;

public class AppleFallGame3 : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasFallen = false;
    private Vector2 initialPosition; // Store the original position

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Keep apple still at start
        initialPosition = transform.position; // Store initial position
    }

    void OnMouseDown()
    {
        if (!hasFallen)
        {
            rb.gravityScale = 1; // Enable gravity
            hasFallen = true;

            // Ignore collisions with other apples
            Collider2D myCollider = GetComponent<Collider2D>();
            Collider2D[] allApples = FindObjectsOfType<Collider2D>();

            foreach (Collider2D apple in allApples)
            {
                if (apple.gameObject.CompareTag("Apple")) // Ensure all apples have this tag
                {
                    Physics2D.IgnoreCollision(myCollider, apple);
                }
            }
        }
    }

    // Reset method to return apple to its initial state
    public void ResetApple()
    {
        transform.position = initialPosition; // Move apple back to start
        rb.gravityScale = 0; // Stop gravity
        rb.linearVelocity = Vector2.zero; // Stop any movement
        hasFallen = false;
    }
}
