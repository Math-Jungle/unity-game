using UnityEngine;

public class PandaController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CollectApple()
    {
        // Play the collect animation
        animator.SetTrigger("Collect");
    }

    public void PuzzledReaction()
    {
        // Play the puzzled animation
        animator.SetTrigger("Puzzled");
    }
}
