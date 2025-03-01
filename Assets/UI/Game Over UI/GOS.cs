using UnityEngine;

public class GOS : MonoBehaviour
{

    public int score = 0;
    public Animator star_1_animator;
    public string animationTriggerName = "PlayAnim";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        if (score >= 3000)
        {
            star_1_animator.SetTrigger(animationTriggerName);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}
