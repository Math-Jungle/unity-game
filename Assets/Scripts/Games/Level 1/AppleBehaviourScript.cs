using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class AppleBehaviourScript : MonoBehaviour
{

    public new Rigidbody2D rigidbody2D;
    private Level_1_GameManager gameManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindFirstObjectByType<Level_1_GameManager>(); // Find the Game Manager
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTouched()
    {
        Debug.Log("Apple Touched");
        rigidbody2D.gravityScale = 1;

        if (gameManager != null)
        {
            gameManager.RegisterAppleTouch();
        }
    }
}
