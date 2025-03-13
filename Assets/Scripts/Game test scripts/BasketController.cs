using UnityEngine;

public class BasketController : MonoBehaviour
{
    public int applesCollected = 0;

    public void AddApple()
    {
        applesCollected++;
        Debug.Log("Apples Collected: " + applesCollected);
        // Update the basket UI or animation if needed
    }
}
