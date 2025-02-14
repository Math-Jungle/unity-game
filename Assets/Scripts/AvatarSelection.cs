using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AvatarSelection : MonoBehaviour
{
    public int avatarID; // Assign different IDs to different avatars
    public bool isNextButton = false; // To differentiate avatars from the next button

    void Start()
    {
        // Check if this script is attached to an avatar or the Next button
        Button button = GetComponent<Button>();
        if (button != null)
        {
            if (isNextButton)
                button.onClick.AddListener(OnNextButtonClicked);
            else
                button.onClick.AddListener(OnAvatarSelected);
        }
    }

    void OnAvatarSelected()
    {
        PlayerPrefs.SetInt("SelectedAvatar", avatarID); // Save selection
        Debug.Log("Avatar Selected: " + avatarID);
    }

    void OnNextButtonClicked()
    {
        Debug.Log("Next button clicked! Loading next scene...");

        // Load the next scene (Replace "NextSceneName" with your actual scene name)
        SceneManager.LoadScene("NextSceneName");
    }
}
