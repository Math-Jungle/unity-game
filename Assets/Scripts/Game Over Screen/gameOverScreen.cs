using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    public int score; // Default score (now private)
    [Header("Animations")]
    public Animator star_1_animator;
    public Animator star_2_animator;
    public Animator star_3_animator;
    public Animator flareAnimator;
    public Animator flareStarsAnimator;
    public Animator blurAnimator;
    public string animationTriggerName = "PlayAnim";
    public string startAnimationTriggerName = "start";

    [Header("UI Components")]
    public TextMeshProUGUI gameScoreUI;
    public RectTransform gameOverScreenTransform;

    public void Start()
    {
        CheckReferences();
        gameOverScreenTransform.localScale = Vector3.zero; // Start with the scale set to zero

    }

    // Method to set the score and calculate stars
    public void EndGame(int playerScore)
    {
        Debug.Log("EndGame() called. Updating score and starting animations.");
        gameObject.SetActive(true); // Enable the game over screen

        Time.timeScale = 0; // Pause the game

        // Update the score UI
        score = playerScore;
        if (gameScoreUI != null)
        {
            gameScoreUI.text = score.ToString();
            Debug.Log("game score updated");
        }

        // Play flare and stars animations
        PlayFlareAndStarsAnimations();


        // Animate the game over screen appearance using LeanTween
        LeanTween.scale(gameOverScreenTransform, Vector3.one * 0.8f, 1f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
        {
            Debug.Log("Game over screen animation completed.");
            StartCoroutine(PlayAnimation(playerScore));
        });

    }

    private void CheckReferences()
    {

        Debug.Log("Checking references.");

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
        if (gameScoreUI != null)
        {
            Debug.Log("Found TextMeshProUGUI component on 'Score' GameObject.");
        }
        else
        {
            Debug.LogError("GameObject named 'Score' not found.");
        }

        // Initialize the game over screen transform
        if (gameOverScreenTransform != null)
        {
            Debug.Log("Found RectTransform component on 'gameOverScreen' GameObject.");
        }
        else
        {
            Debug.LogError("RectTransform component not found on 'gameOverScreen' GameObject.");
        }


    }

    private void PlayFlareAndStarsAnimations()
    {
        // Play blur animation
        if (blurAnimator != null)
        {
            Debug.Log("Playing blur animation.");
            blurAnimator.SetTrigger(startAnimationTriggerName);
        }
        else
        {
            Debug.LogError("blurAnimator is null.");
        }

        // Play flare animation
        if (flareAnimator != null)
        {
            Debug.Log("Playing flare animation.");
            flareAnimator.SetTrigger(startAnimationTriggerName);
        }
        else
        {
            Debug.LogError("flareAnimator is null.");
        }

        // Play flare stars animation
        if (flareStarsAnimator != null)
        {
            Debug.Log("Playing flare stars animation.");
            flareStarsAnimator.SetTrigger(startAnimationTriggerName);
        }
        else
        {
            Debug.LogError("flareStarsAnimator is null.");
        }


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

    /*
        Game over screen button methods
    */
    public void NextLevel(string nextSceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextSceneName);
    }

    public void NextGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        UIManager.instance.LoadNextGame(score);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Home");
    }

}