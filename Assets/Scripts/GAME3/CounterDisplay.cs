using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class CounterDisplay : MonoBehaviour
{
    public TextMeshProUGUI counterText; // Reference to the Text component
    public static int redAppleClickedCount = 0; // Static variables for counts
    public static int greenAppleClickedCount = 0;

    void Update()
    {
        Debug.Log($"Counter Text Updating: GREEN - {AppleFallGame3.greenAppleClickedCount}, RED - {AppleFallGame3.redAppleClickedCount}");
    counterText.text = $"GREEN - {AppleFallGame3.greenAppleClickedCount}\nRED - {AppleFallGame3.redAppleClickedCount}";
    }
}
