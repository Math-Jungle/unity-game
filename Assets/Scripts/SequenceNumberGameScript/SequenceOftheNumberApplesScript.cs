using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SequenceOftheNumberApplesScript : MonoBehaviour, IGameManager
{
    public Rigidbody2D rigidbody2D;
    public int appleNumber; // The number displayed on the apple
    private static int currentSequence = 1; // Keep track of the expected number
    private static int score = 0; // Player's score
    public TextMeshProUGUI totalScore; // UI Text to display the score
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public Dialog dialogBox;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (rigidbody2D == null){
            Debug.LogError("Rigidbody2D is missing");
        }
        rigidbody2D.gravityScale = 0; // Prevent apples from falling initially
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null){
            Debug.LogWarning("AudioSource not found! Adding one.");
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null){
            Debug.LogError("SpriteRenderer is missing!");
        }
        UIManager.instance.ShowUIScreens();
        dialogBox.RunEvent(0, () =>{
            StartGame();
        });
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
            score += 1000;
            currentSequence++; // Move to the next number
            PlaySound(correctSound);

            if (currentSequence > 12)
            {
                Victory();
            }
        }
        else
        {
            Debug.Log("Wrong apple touched! Number: " + appleNumber);
            score -= 500; // Deduct points for incorrect touch
            score = Mathf.Max(score, 0); // Ensure score doesn't go negative
            PlaySound(wrongSound);
            HighlightApple(Color.red, 0.5f);
        }

        UpdateScore();
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

    void Victory()
    {
        EndGame();
        Debug.Log("You have successfully completed the sequence number game");
        Invoke(nameof(ResetGame), 3f);
    }

    void UpdateScore()
    {
        if (totalScore != null){
            totalScore.text = $"Score - {score}";
        }
        else {
            Debug.LogError("totalScore for TextMeshProUGUI is not assigned");
        }
    }

    void ResetGame()
    {
        currentSequence = 1;
        score = 0;
        UpdateScore();
    }

    public void StartGame(){

    }

    public void EndGame(){
        GameManager.Instance.SubmitGameResults(score, null, 0);
    }
}
