using UnityEngine;

public class PandaEatApple : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Apple"))
        {
            Destroy(other.gameObject); // Destroy the apple to simulate eating
            Debug.Log("Panda ate the apple!");
        }
    }
}
