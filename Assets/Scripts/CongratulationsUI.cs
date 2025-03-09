using UnityEngine;
using UnityEngine.SceneManagement;

public class CongratulationsUI : MonoBehaviour
{
    // Call this method from the "Let's Go" button OnClick event
    public void OnLetsGoButtonClicked()
    {
        // Make sure "Home" is added to Build Settings and spelled exactly the same
        SceneManager.LoadScene("Home");
    }
}
