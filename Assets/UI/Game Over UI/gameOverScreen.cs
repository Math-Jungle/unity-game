using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverScreen : MonoBehaviour
{

    public int score = 6000;
    private Animator star_1_animator;
    private Animator star_2_animator;
    private Animator star_3_animator;
    private string animationTriggerName = "PlayAnim";

    public Scene scene;

    private TextMeshProUGUI gameScoreUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log($"Score: {score}");

        if (star_1_animator == null)
        {
            star_1_animator = GameObject.Find("Star_1")?.GetComponent<Animator>();
        }
        if (star_2_animator == null)
        {
            star_2_animator = GameObject.Find("Star_2")?.GetComponent<Animator>();
        }
        if (star_3_animator == null)
        {
            star_3_animator = GameObject.Find("Star_3")?.GetComponent<Animator>();
        }

        // Debug if animators were found
        Debug.Log($"Star 1 Animator: {star_1_animator}");
        Debug.Log($"Star 2 Animator: {star_2_animator}");
        Debug.Log($"Star 3 Animator: {star_3_animator}");

        if (star_1_animator == null || star_2_animator == null || star_3_animator == null)
        {
            Debug.LogError("One or more star animators are still not assigned!");
            return;
        }

        Debug.Log("Animators assigned successfully.");

        StartCoroutine(PlayAnimation());

        // if (score >= 3000)
        // {
        //     Debug.Log("Score is greater than 3000, 1st animation will be played.");
        //     star_1_animator.SetTrigger(animationTriggerName);
        // }
        // if (score >= 5000)
        // {
        //     star_2_animator.SetTrigger(animationTriggerName);
        // }
        // if (score >= 7000)
        // {
        //     star_3_animator.SetTrigger(animationTriggerName);
        // }
        // else
        // {
        //     Debug.Log("Score is less than 3000, no animation will be played.");
        // }

        gameScoreUI = GameObject.Find("Score")?.GetComponent<TextMeshProUGUI>();
        if (gameScoreUI != null)
        {
            gameScoreUI.text = score.ToString();
            Debug.Log($"Score set to: {score}");
        }
        else
        {
            Debug.LogError("GameScoreUI TextMeshProUGUI component not found!");
        }
    }



    // Update is called once per frame
    void Update()
    {
        // if (gameScoreUI != null)
        // {
        //     gameScoreUI.text = score.ToString();
        // }
        // else
        // {
        //     Debug.LogError("GameScoreUI TextMeshProUGUI component not found!");
        // }
    }

    private IEnumerator PlayAnimation()
    {
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
        else
        {
            Debug.Log("Score is less than 3000, no animation will be played.");
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Game1");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Game1");
    }

    public void Home()
    {
        SceneManager.LoadScene("Home");
    }
}
