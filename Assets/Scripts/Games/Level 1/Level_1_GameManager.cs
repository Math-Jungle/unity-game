using System.Collections.Generic;
using UnityEngine;

public class Level_1_GameManager : MonoBehaviour, IGameManager
{
    public Dialog dialog;
    private float startTime;
    private float endTime;
    private List<float> reactionTimes = new List<float>();
    private int gameScore = 0;
    private float lastTouchTime = 0f;
    private int applesPicked = 0;
    public float expectedReactionTime = 5f;
    private bool isGameOver = false; // Flag to prevent multiple calls to EndGame  public AudioClip appleHitGroundSound; // Sound when the apple hits the ground
    // private AudioSource audioSource; // AudioSource for sound effects

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // // Initialize AudioSource
        // audioSource = GetComponent<AudioSource>();
        // if (audioSource == null)
        // {
        //     Debug.LogError("AudioSource component not found on the GameManager!");
        // }

        UIManager.instance.ShowUIScreens();

        dialog.RunEvent(0, () =>
        {
            StartGame();
        });
    }

    // Update is called once per frame
    void Update()
    {
        // Handle touches
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                if (hit.collider != null)
                {
                    Debug.Log("Raycast hit: " + hit.collider.gameObject.name);

                    // Check if the hit object is an apple
                    AppleBehaviourScript apple = hit.collider.GetComponent<AppleBehaviourScript>();
                    if (apple != null)
                    {
                        apple.OnTouched();

                    }
                }
            }
        }

        if (applesPicked >= 10 && !isGameOver)
        {
            EndGame();
        }
    }

    public void RegisterAppleTouch()
    {
        if (isGameOver)
        {
            return; // Prevent registering touches after the game is over
        }

        float reactionTime = Time.time - lastTouchTime;
        lastTouchTime = Time.time;
        reactionTimes.Add(reactionTime);
        applesPicked++;

        //Score calculation
        gameScore += 700;
        if (reactionTime <= expectedReactionTime)
        {
            gameScore += 300;
        }
    }

    public void StartGame()
    {
        Debug.Log("Game Started!");
        // PlaySound();
        startTime = Time.time;
        lastTouchTime = Time.time;
        gameScore = 0; // Reset score
        reactionTimes.Clear(); // Clear previous reaction times
        applesPicked = 0;
    }

    public void EndGame()
    {
        isGameOver = true;

        endTime = Time.time;
        float gameDuration = endTime - startTime; // Calculate gameTime

        //Saving LevelData 
        GameManager.Instance.SubmitGameResults(gameScore, reactionTimes, gameDuration);
        Debug.Log($"Submitted results: Score: {gameScore}, ReactionTimes: {reactionTimes}, GameTime: {gameDuration}");

        //Sending game data to the backend
    }

    // Method to play a sound
    // private void PlaySound()
    // {
    //     if (audioSource != null)
    //     {
    //         audioSource.Play();
    //     }
    //     else
    //     {
    //         Debug.LogWarning("AudioSource or AudioClip is missing!");
    //     }
    // }

}