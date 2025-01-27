using UnityEngine;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{
    // References to the splash screen and login page canvases
    public GameObject splash;
    public GameObject login;

    // Button to trigger the transition
    public Button transitionButton;

    void Start()
    {
        // Ensure the splash screen is active and login page is inactive initially
        splash.SetActive(true);
        login.SetActive(false);

        // Add a listener to the button to call the TransitionToLogin method when clicked
        transitionButton.onClick.AddListener(TransitionToLogin);
    }

    void TransitionToLogin()
    {
        // Hide the splash screen and show the login page
        splash.SetActive(false);
        login.SetActive(true);
    }
}
