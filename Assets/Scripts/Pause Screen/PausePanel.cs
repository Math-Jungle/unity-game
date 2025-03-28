using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    public GameObject pausePanel;
    public RectTransform pausePanelTransform;
    public Volume globalVolume;
    public Button pauseButton;

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
        pauseButton.interactable = false;
    }

    public void Resume()
    {
        Debug.Log("Resume method called");
        LeanTween.scale(pausePanelTransform, Vector3.zero, animationDuration).setIgnoreTimeScale(true).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
        {
            pausePanel.SetActive(false);

        });
        Time.timeScale = 1;
        pauseButton.interactable = true;
    }

    public void Home()
    {
        Debug.Log("Home method called");
        Time.timeScale = 1; // Ensure time scale is reset to normal
        pausePanel.SetActive(false);
        SceneManager.LoadScene("Home");
    }
}
