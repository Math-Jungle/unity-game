using UnityEngine;

public class StarDisplay : MonoBehaviour
{
    [Header("Star Objects")]
    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;

    public void DisplayStars(int starCount)
    {
        // Validate the star count
        if (starCount < 0 || starCount > 3)
        {
            Debug.LogError("Invalid star count. Must be between 0 and 3.");
            return;
        }

        if (starCount >= 1) star1.SetActive(true);
        if (starCount >= 2) star2.SetActive(true);
        if (starCount >= 3) star3.SetActive(true);
    }
}
