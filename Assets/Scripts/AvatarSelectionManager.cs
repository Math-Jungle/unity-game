using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class AvatarSelectionManager : MonoBehaviour
{
    public Button[] avatarButtons; // Assign all 6 buttons in Inspector
    public Button nextButton;

    private string selectedAvatar = "";

    // Add UI elements for registration status
    public GameObject loadingPanel;
    public TextMeshProUGUI statusText;

    void Start()
    {
        nextButton.interactable = false;

        if (loadingPanel != null)
            loadingPanel.SetActive(false);

        // Add listeners for all avatar buttons
        for (int i = 0; i < avatarButtons.Length; i++)
        {
            string avatarId = "avatar_" + i;
            avatarButtons[i].onClick.AddListener(() => SelectAvatar(avatarId));
        }
    }

    void SelectAvatar(string avatarId)
    {
        selectedAvatar = avatarId;
        Debug.Log("Selected Avatar: " + selectedAvatar);

        // Reset visual selection for all buttons
        foreach (Button button in avatarButtons)
        {
            button.GetComponent<Image>().color = Color.white;
        }

        // Highlight selected button
        for (int i = 0; i < avatarButtons.Length; i++)
        {
            if ("avatar_" + i == avatarId)
            {
                avatarButtons[i].GetComponent<Image>().color = Color.green;
                break;
            }
        }

        nextButton.interactable = true;
    }

    public void OnNextButtonClicked()
    {
        if (!string.IsNullOrEmpty(selectedAvatar))
        {
            // Store avatar selection
            UserDataManager.Instance.SetAvatarId(selectedAvatar);

            // Show loading screen
            if (loadingPanel != null)
            {
                loadingPanel.SetActive(true);
                statusText.text = "Registering user...";
            }

            // Send registration data to backend
            StartCoroutine(UserDataManager.Instance.RegisterUser(OnRegistrationComplete));
        }
    }

    private void OnRegistrationComplete(bool success, string message)
    {
        if (success)
        {
            // Registration successful, proceed to next scene
            if (statusText != null)
                statusText.text = "Registration successful!";

            // Wait a moment to show success message
            StartCoroutine(LoadNextSceneAfterDelay(1.0f));
        }
        else
        {
            // Registration failed, show error
            if (statusText != null)
                statusText.text = "Registration failed: " + message;

            // Hide loading panel after delay
            StartCoroutine(HideLoadingPanelAfterDelay(3.0f));
        }
    }

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("nextScene"); // Replace with your home/main scene
    }

    private IEnumerator HideLoadingPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (loadingPanel != null)
            loadingPanel.SetActive(false);
    }
}