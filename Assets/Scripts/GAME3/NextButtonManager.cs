using UnityEngine;
using UnityEngine.UI;

public class NextButtonManager : MonoBehaviour
{
    public Button nextButton;  // The button to trigger the check
    public AppleFallGame3 appleGame;  // Reference to the AppleFallGame3 script

    void Start()
    {
        // Add listener to the button to call the method when clicked
        nextButton.onClick.AddListener(CheckAndDisplayMessage);
    }

    void CheckAndDisplayMessage()
    {
        // Check if the picked red and green apples are correct
        if (AppleFallGame3.redAppleClickedCount == AppleFallGame3.requiredRedApples && 
            AppleFallGame3.greenAppleClickedCount == AppleFallGame3.requiredGreenApples)
        {
            Debug.Log("Good job! You picked the right number of apples.");
        }
        else
        {
            Debug.Log("Try again! The numbers of apples are not correct.");
        }
    }
}
