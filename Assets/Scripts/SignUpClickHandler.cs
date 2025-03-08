using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems; // Required for IPointerClickHandler

public class SignUpClickHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        if (text != null)
        {
            Debug.Log("Sign Up TMP Found!");
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Sign Up clicked! Loading account creation scene...");
        // Ensure "login" matches your scene name.
        SceneManager.LoadScene("login");
    }
}
