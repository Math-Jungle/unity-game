using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems; // Required for IPointerClickHandler

public class SignUpClickHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Get the TextMeshPro component and add a click listener
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        
        if (text != null)
        {
            Debug.Log("Sign Up TMP Found!");
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Sign Up clicked! Loading acc creation scene...");
        SceneManager.LoadScene("acc creation");
    }

  
        
}
