using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AvatarSelectionManager : MonoBehaviour
{
    // Six avatar buttons � assign these in the Inspector.
    public Button avatarButton1; // e.g., first avatar option
    public Button avatarButton2; // e.g., second avatar option
    public Button avatarButton3; // e.g., third avatar option
    public Button avatarButton4; // e.g., fourth avatar option
    public Button avatarButton5; // e.g., fifth avatar option
    public Button avatarButton6; // e.g., sixth avatar option

    public Button nextButton;    // Button to proceed after selection

    // Reference to the RegistrationManager script (assign via Inspector)
    public RegistrationManager registrationManager;

    private string selectedAvatar = ""; // Stores the selected avatar identifier

    void Start()
    {
        nextButton.interactable = false; // Disable Next button until an avatar is selected

        // Set up click listeners for each avatar button with unique identifiers
        avatarButton1.onClick.AddListener(() => SelectAvatar("avatar1"));
        avatarButton2.onClick.AddListener(() => SelectAvatar("avatar2"));
        avatarButton3.onClick.AddListener(() => SelectAvatar("avatar3"));
        avatarButton4.onClick.AddListener(() => SelectAvatar("avatar4"));
        avatarButton5.onClick.AddListener(() => SelectAvatar("avatar5"));
        avatarButton6.onClick.AddListener(() => SelectAvatar("avatar6"));
    }

    void SelectAvatar(string avatar)
    {
        selectedAvatar = avatar;
        Debug.Log("Selected Avatar: " + selectedAvatar);
        nextButton.interactable = true;
    }

    public void OnNextButtonClicked()
    {
        if (!string.IsNullOrEmpty(selectedAvatar))
        {
            // Save the selected avatar using PlayerPrefs
            PlayerPrefs.SetString("SelectedAvatar", selectedAvatar);
            PlayerPrefs.Save();

            // Retrieve all stored registration data
            string email = PlayerPrefs.GetString("UserEmail");
            string password = PlayerPrefs.GetString("UserPassword");
            string childName = PlayerPrefs.GetString("ChildName");
            int childAge = PlayerPrefs.GetInt("ChildAge");
            string avatar = PlayerPrefs.GetString("SelectedAvatar");

            // Call the registration coroutine to send data to the backend
            StartCoroutine(registrationManager.RegisterNewUser(email, password, childName, childAge, avatar));

            // Optionally, after successful registration, load another scene.
            SceneManager.LoadScene("Congratulation");
        }
    }
}
