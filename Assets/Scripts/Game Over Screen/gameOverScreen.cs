using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    private int score; // Default score (now private)
    private Animator star_1_animator;
    private Animator star_2_animator;
    private Animator star_3_animator;
    private string animationTriggerName = "PlayAnim";

    private TextMeshProUGUI gameScoreUI;
    private RectTransform gameOverScreenTransform;

    // Method to set the score and calculate stars
    public void EndGame(int playerScore)
    {
        Debug.Log("EndGame() called. Updating score and starting animations.");

        Time.timeScale = 0; // Pause the game

        // Update the score UI
        score = playerScore;
        if (gameScoreUI != null)
        {
            gameScoreUI.text = score.ToString();
            Debug.Log("game score updated");
        }



        // Animate the game over screen appearance using LeanTween
        LeanTween.scale(gameOverScreenTransform, Vector3.one * 0.8f, 1f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
        {
            Debug.Log("Game over screen animation completed.");
            StartCoroutine(PlayAnimation(playerScore));
        });

    }

    public void Start()
    {

        Debug.Log("Start() called. Initializing references.");

        // Initialize animators
        star_1_animator = GameObject.Find("Star_1")?.GetComponent<Animator>();
        star_2_animator = GameObject.Find("Star_2")?.GetComponent<Animator>();
        star_3_animator = GameObject.Find("Star_3")?.GetComponent<Animator>();

        // Set animators to use unscaled time
        if (star_1_animator != null)
        {
            star_1_animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
        if (star_2_animator != null)
        {
            star_2_animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
        if (star_3_animator != null)
        {
            star_3_animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }

        // Initialize score UI
        GameObject scoreGameObject = GameObject.Find("Score");
        if (scoreGameObject != null)
        {
            Debug.Log("Found GameObject named 'Score'.");
            gameScoreUI = scoreGameObject.GetComponent<TextMeshProUGUI>();
            if (gameScoreUI != null)
            {
                Debug.Log("Found TextMeshProUGUI component on 'Score' GameObject.");
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found on 'Score' GameObject.");
            }
        }
        else
        {
            Debug.LogError("GameObject named 'Score' not found in the scene.");
        }

        // Initialize the game over screen transform
        gameOverScreenTransform = GetComponent<RectTransform>();

        if (gameOverScreenTransform != null)
        {
            Debug.Log("Found RectTransform component on 'gameOverScreen' GameObject.");
        }
        else
        {
            Debug.LogError("RectTransform component not found on 'gameOverScreen' GameObject.");
        }

        gameOverScreenTransform.localScale = Vector3.zero; // Start with the scale set to zero


    }

    private IEnumerator PlayAnimation(int playerScore)
    {
        score = playerScore;
        //yield return new WaitForSecondsRealtime(1);

        if (score >= 3000)
        {
            Debug.Log("Score is greater than 3000, 1st animation will be played.");
            yield return new WaitForSecondsRealtime(0.1f);
            star_1_animator.SetTrigger(animationTriggerName);
        }
        if (score >= 5000)
        {
            yield return new WaitForSecondsRealtime(0.6f);
            star_2_animator.SetTrigger(animationTriggerName);
        }
        if (score >= 7000)
        {
            yield return new WaitForSecondsRealtime(0.6f);
            star_3_animator.SetTrigger(animationTriggerName);
        }
        else if (score < 3000 && score >= 0)
        {
            Debug.Log("Score is less than 3000, no animation will be played.");
        }
    }
}