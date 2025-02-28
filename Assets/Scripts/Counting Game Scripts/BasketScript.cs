using UnityEngine;
using TMPro; // Required for TextMeshPro

public class BasketScript : MonoBehaviour
{
    public int requiredApples; // Number of apples required for this basket
    private int currentApples = 0; // Current number of apples in the basket
    public TextMeshProUGUI appleCountText; // Reference to the TextMeshPro component

    private void Start()
    {
        // Initialize the UI text
        UpdateAppleCountText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Apple"))
        {
            currentApples++;
            UpdateAppleCountText(); // Update the UI text

            // Disable the apple so it doesn't interact further
            collision.gameObject.SetActive(false);

            // Check if the basket has the required number of apples
            if (currentApples >= requiredApples)
            {
                Debug.Log("Basket has the required number of apples!");
                // You can add additional logic here, such as triggering a win condition
            }
        }
    }

    private void UpdateAppleCountText()
    {
        if (appleCountText != null)
        {
            appleCountText.text = "Apples: " + currentApples;
        }
    }
}