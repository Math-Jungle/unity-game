using UnityEngine;

public class AppleBehaviourScript : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    private Level_1_GameManager gameManager;

    public AudioClip appleSelectSound; // Sound when the apple is clicked
    public AudioClip appleHitSound; // Sound when the apple hits the ground
    private AudioSource audioSource; // AudioSource for sound effects

    private bool hasHitGround = false; // Track if the apple has hit the ground

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0; // Prevent the apple from falling initially

        gameManager = FindFirstObjectByType<Level_1_GameManager>(); // Find the Game Manager

        // Initialize AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the apple!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Optional: Add touch or mouse input support if needed
    }

    public void OnTouched()
    {
        Debug.Log("Apple Touched");
        rigidbody2D.gravityScale = 1;

        // Play sound when the apple is clicked
        PlaySound(appleSelectSound);

        if (gameManager != null)
        {
            gameManager.RegisterAppleTouch();
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

    // Method to play a sound
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