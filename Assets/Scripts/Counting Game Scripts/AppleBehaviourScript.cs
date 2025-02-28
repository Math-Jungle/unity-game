using UnityEngine;

public class AppleBehaviourScript : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    private bool isDragging = false; // Track if the apple is being dragged
    private Vector2 touchOffset; // Offset to prevent snapping to touch position

    // Start is called before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0; // Prevent the apple from falling initially
    }

    // Update is called once per frame
    void Update()
    {
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
                        touchOffset = (Vector2)transform.position - touchPosition; // Calculate offset
                        rigidbody2D.gravityScale = 0; // Disable gravity while dragging
                        rigidbody2D.linearVelocity = Vector2.zero; // Stop any existing movement
                        Debug.Log("Apple Touched! Dragging started.");
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
                        Debug.Log("Apple Released! Gravity enabled.");
                    }
                    break;
            }
        }
    }
}