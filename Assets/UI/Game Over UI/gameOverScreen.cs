using System.Collections;
using TMPro;
using UnityEngine;

public class gameOverScreen : MonoBehaviour
{
    private int score; // Default score (now private)
    private Animator star_1_animator;
    private Animator star_2_animator;
    private Animator star_3_animator;
    private string animationTriggerName = "PlayAnim";

    private TextMeshProUGUI gameScoreUI;

    // Method to set the score and calculate stars
    public void SetScore(int playerScore)
    {
        Debug.Log("SetScore() called. Updating score and starting animations.");
        score = playerScore;
        StartCoroutine(PlayAnimation(playerScore));
        Debug.Log(gameScoreUI == null);
        // Update the score UI
        if (gameScoreUI != null)
        {
            gameScoreUI.text = score.ToString();
            Debug.Log("gameScoreUI.text occured");
        }
    }

   public void Start()
{

    Debug.Log("Start() called. Initializing references.");
    // Initialize animators
    star_1_animator = GameObject.Find("Star_1")?.GetComponent<Animator>();
    star_2_animator = GameObject.Find("Star_2")?.GetComponent<Animator>();
    star_3_animator = GameObject.Find("Star_3")?.GetComponent<Animator>();

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
}

    private IEnumerator PlayAnimation(int playerScore)
    {
        score = playerScore;
        yield return new WaitForSeconds(1);

        if (score >= 3000)
        {
            Debug.Log("Score is greater than 3000, 1st animation will be played.");
            yield return new WaitForSeconds(0.1f);
            star_1_animator.SetTrigger(animationTriggerName);
        }
        if (score >= 5000)
        {
            yield return new WaitForSeconds(0.6f);
            star_2_animator.SetTrigger(animationTriggerName);
        }
        if (score >= 7000)
        {
            yield return new WaitForSeconds(0.6f);
            star_3_animator.SetTrigger(animationTriggerName);
        }
        else if(score < 3000 && score >= 0)
        {
            Debug.Log("Score is less than 3000, no animation will be played.");
        }
    }
}