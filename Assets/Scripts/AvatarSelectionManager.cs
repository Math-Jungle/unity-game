using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AvatarSelectionManager : MonoBehaviour
{
    public Button girlAvatarButton; // Assign in Inspector
    public Button boyAvatarButton;  // Assign in Inspector
    public Button nextButton;       // Assign in Inspector

    private string selectedAvatar = ""; // Stores the selected avatar

    void Start()
    {
        nextButton.interactable = false; // Disable Next button at start

        // Add click listeners
        girlAvatarButton.onClick.AddListener(() => SelectAvatar("girl"));
        boyAvatarButton.onClick.AddListener(() => SelectAvatar("boy"));
    }

    void SelectAvatar(string avatar)
    {
        selectedAvatar = avatar;
        Debug.Log("Selected Avatar: " + selectedAvatar);

        // Enable Next button after selection
        nextButton.interactable = true;
    }

    public void OnNextButtonClicked()
    {
        if (!string.IsNullOrEmpty(selectedAvatar))
        {
            PlayerPrefs.SetString("SelectedAvatar", selectedAvatar); // Store selection
            PlayerPrefs.Save();
            SceneManager.LoadScene("nextScene"); // Change "nextScene" to the correct name
        }
    }
}
