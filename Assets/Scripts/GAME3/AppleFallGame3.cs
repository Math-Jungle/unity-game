using UnityEngine;

public class AppleFallGame3 : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasFallen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Keep apple still at start
    }

    void OnMouseDown()
    {
        if (!hasFallen) // Only fall once
        {
            rb.gravityScale = 1; // Enable gravity to make apple fall
            hasFallen = true;
        }
    }
}

