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
            // Store child info
            UserDataManager.Instance.SetChildInfo(childName, int.Parse(childAge));

            SceneManager.LoadScene("avatar");
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

        return true;
    }
}