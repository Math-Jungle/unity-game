using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SetUpManagerToAvatar : MonoBehaviour
{
    public TMP_InputField childNameInput;
    public TMP_InputField childAgeInput;

    public void OnNextButtonClicked()
    {
        string childName = childNameInput.text.Trim();
        string childAge = childAgeInput.text.Trim();

        if (IsValidInput(childName, childAge))
        {
            SceneManager.LoadScene("avatar"); // Ensure "avatar" is the correct scene name
        }
        else
        {
            Debug.Log("Invalid input! Ensure the child's name is entered and the age is a number.");
        }
    }

    private bool IsValidInput(string name, string age)
    {
        if (string.IsNullOrEmpty(name))
        {
            return false; // Name cannot be empty
        }

        if (!int.TryParse(age, out int ageValue) || ageValue <= 0)
        {
            return false; // Age must be a valid positive number
        }

        return true; // Ensure the function returns true when valid
    }
}
