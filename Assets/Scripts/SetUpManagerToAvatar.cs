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
            // Save child info for later use
            PlayerPrefs.SetString("ChildName", childName);
            int ageValue = int.Parse(childAge);
            PlayerPrefs.SetInt("ChildAge", ageValue);
            PlayerPrefs.Save();

            // Ensure "avatar" is the exact name of your avatar selection scene.
            SceneManager.LoadScene("avatar");
        }
        else
        {
            Debug.Log("Invalid input! Please ensure you enter a child's name and a valid, positive age.");
        }
    }

    private bool IsValidInput(string name, string age)
    {
        if (string.IsNullOrEmpty(name))
            return false;
        if (!int.TryParse(age, out int ageValue) || ageValue <= 0)
            return false;
        return true;
    }
}
