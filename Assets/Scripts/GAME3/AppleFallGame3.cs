using UnityEngine;
using System.Collections.Generic;

public class AppleFallGame3 : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasFallen = false;
    private Vector2 initialPosition;

    // Static list to track all apples
    public static List<AppleFallGame3> allApples = new List<AppleFallGame3>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        initialPosition = transform.position;

        // Register this apple in the list
        allApples.Add(this);
    }

    void OnDestroy()
    {
        // Remove apple from the list when destroyed
        allApples.Remove(this);
    }

    void OnMouseDown()
    {
        if (!hasFallen)
        {
            rb.gravityScale = 1;
            hasFallen = true;

            Collider2D myCollider = GetComponent<Collider2D>();
            Collider2D[] allColliders = FindObjectsOfType<Collider2D>();

            foreach (Collider2D apple in allColliders)
            {
                if (apple.gameObject.CompareTag("Apple"))
                {
                    Physics2D.IgnoreCollision(myCollider, apple);
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
}
