using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public GameObject pausePanel;
    public RectTransform pausePanelTransform;
    public Volume globalVolume;

    public float animationDuration = 0.5f;

    void Start()
    {
        pausePanel.SetActive(false);
        pausePanelTransform.localScale = Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pause()
    {
        Debug.Log("Pause method called");
        pausePanel.SetActive(true);
        LeanTween.scale(pausePanelTransform, Vector3.one, animationDuration).setIgnoreTimeScale(true).setEase(LeanTweenType.easeOutBack);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Debug.Log("Resume method called");
        LeanTween.scale(pausePanelTransform, Vector3.zero, animationDuration).setIgnoreTimeScale(true).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
        {
            pausePanel.SetActive(false);

        });
        Time.timeScale = 1;
    }

    public void Home()
    {
        Debug.Log("Home method called");
        Time.timeScale = 1; // Ensure time scale is reset to normal
        SceneManager.LoadScene("Home");
    }
}
