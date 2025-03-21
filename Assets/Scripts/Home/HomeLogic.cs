using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HomeLogic : MonoBehaviour
{
    [SerializeField] private TMP_Text welcomeMessage;
    [SerializeField] private Image avatarImage;

    [Header("Avatar Sprites")]
    [SerializeField] private Sprite avatar1Sprite;
    [SerializeField] private Sprite avatar2Sprite;
    [SerializeField] private Sprite avatar3Sprite;
    [SerializeField] private Sprite avatar4Sprite;
    [SerializeField] private Sprite avatar5Sprite;
    [SerializeField] private Sprite avatar6Sprite;

    void Start()
    {
        // Try to get user data from GameManager
        if (GameManager.Instance != null && GameManager.Instance.CurrentUserData != null)
        {
            var userData = GameManager.Instance.CurrentUserData;
            welcomeMessage.text = $"{userData.childName}";
            SetAvatar(userData.avatarId);
        }
        else
        {
            // Fallback: read from PlayerPrefs
            Debug.LogWarning("No user data found in GameManager! Falling back to PlayerPrefs...");

            string childName = PlayerPrefs.GetString("ChildName", "Guest");
            string avatarId = PlayerPrefs.GetString("SelectedAvatar", "avatar1");

            welcomeMessage.text = $"{childName}";
            SetAvatar(avatarId);
        }
    }

    private void SetAvatar(string avatarId)
    {
        switch (avatarId)
        {
            case "avatar1": avatarImage.sprite = avatar1Sprite; break;
            case "avatar2": avatarImage.sprite = avatar2Sprite; break;
            case "avatar3": avatarImage.sprite = avatar3Sprite; break;
            case "avatar4": avatarImage.sprite = avatar4Sprite; break;
            case "avatar5": avatarImage.sprite = avatar5Sprite; break;
            case "avatar6": avatarImage.sprite = avatar6Sprite; break;
            default: avatarImage.sprite = avatar1Sprite; break;
        }
    }

    public void GameMap()
    {
        SceneManager.LoadScene("game map");
    }
}
