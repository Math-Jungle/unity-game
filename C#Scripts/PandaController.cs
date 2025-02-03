using UnityEngine;

public class PandaController : MonoBehaviour{
    private Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
        // Set fixed position on the left side
        transform.position = new Vector3(-7f, -3f, 0f);
    }

    void Update(){
        // Keep animator for visual feedback
        if (animator != null){
            animator.SetBool("IsGrounded", true);
        }
    }
}