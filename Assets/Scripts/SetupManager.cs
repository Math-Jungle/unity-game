using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // Add this for TextMeshPro support

public class SetupManager : MonoBehaviour
{
    public TMP_InputField childNameInput;    // Changed from InputField to TMP_InputField
    public TMP_InputField childAgeInput;     // Changed from InputField to TMP_InputField
    public Button nextButton;

    void Start()
    {
        if (nextButton == null)
        {
            Debug.LogError("Next Button reference is missing!");
            return;
        }
        nextButton.onClick.AddListener(LoadAvatarScene);
    }

    public void LoadAvatarScene()
    {
        Debug.Log("Next button clicked! Loading Avatar Scene...");
        SceneManager.LoadScene("avatar");
    }
}