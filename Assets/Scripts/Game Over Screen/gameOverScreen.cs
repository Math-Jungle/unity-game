using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gameOverScreen : MonoBehaviour
{
    public int score; // Default score 
    private Animator star_1_animator;
    private Animator star_2_animator;
    private Animator star_3_animator;
    private Animator flareAnimator;
    private Animator flareStarsAnimator;
    private Animator blurAnimator;
    private string animationTriggerName = "PlayAnim";
    private string startAnimationTriggerName = "start";

    private TextMeshProUGUI gameScoreUI;
    private RectTransform gameOverScreenTransform;

    public void SetScore(int playerScore)
    {
        Debug.Log("SetScore() called. Updating score and starting animations.");
        gameObject.SetActive(true);
        Time.timeScale = 0;

        score = playerScore;
        if (gameScoreUI != null)
        {
            gameScoreUI.text = score.ToString();
            Debug.Log("gameScoreUI.text updated");
        }

        PlayFlareAndStarsAnimations();
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
        flareAnimator = GameObject.Find("Flare")?.GetComponent<Animator>();
        flareStarsAnimator = GameObject.Find("FlareStars")?.GetComponent<Animator>();
        blurAnimator = GameObject.Find("Blur")?.GetComponent<Animator>();

        // Initialize score UI
        GameObject scoreGameObject = GameObject.Find("Score");
        if (scoreGameObject != null)
        {
            Debug.Log("Found GameObject named 'Score'.");
            gameScoreUI = scoreGameObject.GetComponent<TextMeshProUGUI>();
            if (gameScoreUI == null)
            {
                Debug.LogError("TextMeshProUGUI component not found on 'Score' GameObject.");
            }
        }
        else
        {
            Debug.LogError("GameObject named 'Score' not found in the scene.");
        }

        // Initialize the game over screen transform
        GameObject gameOverScreenObj = GameObject.Find("gameOverScreen");
        if (gameOverScreenObj != null)
        {
            gameOverScreenTransform = gameOverScreenObj.GetComponent<RectTransform>();
            gameOverScreenTransform.localScale = Vector3.zero;
        }
        else
        {
            Debug.LogError("GameObject named 'gameOverScreen' not found in the scene.");
        }
    }

    private void PlayFlareAndStarsAnimations()
    {
        if (blurAnimator != null)
        {
            Debug.Log("Playing blur animation.");
            blurAnimator.SetTrigger(startAnimationTriggerName);
        }
        if (flareAnimator != null)
        {
            Debug.Log("Playing flare animation.");
            flareAnimator.SetTrigger(startAnimationTriggerName);
        }
        if (flareStarsAnimator != null)
        {
            Debug.Log("Playing flare stars animation.");
            flareStarsAnimator.SetTrigger(startAnimationTriggerName);
        }
    }

    private IEnumerator PlayAnimation(int playerScore)
    {
        yield return new WaitForSecondsRealtime(1);

        if (playerScore >= 3000)
        {
            Debug.Log("Score is greater than 3000, 1st animation will be played.");
            yield return new WaitForSecondsRealtime(0.1f);
            star_1_animator.SetTrigger(animationTriggerName);
        }
        if (playerScore >= 5000)
        {
            yield return new WaitForSecondsRealtime(0.6f);
            star_2_animator.SetTrigger(animationTriggerName);
        }
        if (playerScore >= 7000)
        {
            yield return new WaitForSecondsRealtime(0.6f);
            star_3_animator.SetTrigger(animationTriggerName);
        }
        else if (playerScore < 3000 && playerScore >= 0)
        {
            Debug.Log("Score is less than 3000, no animation will be played.");
        }
    }
}