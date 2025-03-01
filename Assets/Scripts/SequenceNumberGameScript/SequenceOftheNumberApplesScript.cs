using UnityEngine;
using UnityEngine.UI;

public class SequenceOftheNumberApplesScript : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public int appleNumber; // The number displayed on the apple
    private static int currentSequence = 1; // Keep track of the expected number
    private static int score = 0; // Player's score
    public Text scoreText; // UI Text to display the score
    public Text feedbackText; // UI Text for feedback messages
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public ParticleSystem celebrationEffect;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0; // Prevent apples from falling initially
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
       foreach (Touch touch in Input.touches)
       {
        if (touch.phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                HandleAppleTouch();
            }
        }
       } 
    }

    void HandleAppleTouch()
    {
        if (appleNumber == currentSequence)
        {
            Debug.Log("Correct apple touched! Number: " + appleNumber);
            rigidbody2D.gravityScale = 1; // This makes the apple fall to the ground
            score += 10;
            currentSequence++; // Move to the next number
            ShowFeedback("Good job!", Color.green);
            PlaySound(correctSound);

            if (currentSequence > 12)
            {
                CelebrateVictory();
            }
        }
        else
        {
            Debug.Log("Wrong apple touched! Number: " + appleNumber);
            score -= 5; // Deduct points for incorrect touch
            score = Mathf.Max(score, 0); // Ensure score doesn't go negative
            ShowFeedback("Try again!", Color.red);
            PlaySound(wrongSound);
            HighlightApple(Color.red, 0.5f);
        }

        UpdateScoreUI();
    }

    void ShowFeedback(string message, Color color)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            feedbackText.color = color;
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
            Debug.Log("Playing sound: " + clip.name);
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing!");
        }
    }

    void HighlightApple(Color color, float duration)
    {
        spriteRenderer.color = color;
        Invoke("ResetAppleColor", duration);
    }

    void ResetAppleColor()
    {
        spriteRenderer.color = Color.white;
    }

    void CelebrateVictory()
    {
        if (celebrationEffect != null)
        {
            celebrationEffect.Play();
        }
        ShowFeedback("You completed the sequence!", Color.yellow);
        Invoke("ResetGame", 3f);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void ResetGame()
    {
        currentSequence = 1;
        score = 0;
    }
}
