using UnityEngine;

public class AppleScript : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    private bool isDragging = false; // Track if the apple is being dragged
    private Vector2 touchOffset; // Offset to prevent snapping to touch position
    private Vector2 initialPosition; // Store the initial position of the apple

    // Start is called before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0; // Prevent the apple from falling initially
        initialPosition = transform.position; // Store the initial position
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

        // Optional: Add mouse input support for testing in the editor
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                touchOffset = (Vector2)transform.position - mousePosition;
                rigidbody2D.gravityScale = 0;
                rigidbody2D.linearVelocity = Vector2.zero;
                Debug.Log("Apple Touched! Dragging started.");
            }
        }

        if (isDragging && Input.GetMouseButton(0)) // Left mouse button held
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition + touchOffset;
        }

        if (isDragging && Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            isDragging = false;
            rigidbody2D.gravityScale = 1;
            Debug.Log("Apple Released! Gravity enabled.");
        }
    }

    // Method to reset the apple to its initial position
    public void ResetApple()
    {
        transform.position = initialPosition; // Restore the initial position
        rigidbody2D.gravityScale = 0; // Disable gravity
        rigidbody2D.linearVelocity = Vector2.zero; // Stop any movement
        gameObject.SetActive(true); // Ensure the apple is active
    }
}