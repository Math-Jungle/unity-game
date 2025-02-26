using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class AppleBehaviourScript : MonoBehaviour
{

    public Rigidbody2D rigidbody2D;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      rigidbody2D = GetComponent<Rigidbody2D>();
      rigidbody2D.gravityScale = 0;// prevent the apple falling initially 
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches){
            if(touch.phase == TouchPhase.Began){
                Debug.Log("Touch detected at position: " + touch.position);

                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Debug.Log("Touch position in world coordinates: " + touchPosition);

                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                Debug.Log("Raycast hit: " + (hit.collider != null ? hit.collider.gameObject.name: "nothing"));

                if(hit.collider != null && hit.collider.gameObject == gameObject){
                    Debug.Log("Apple Touched!");
                    rigidbody2D.gravityScale = 1;
                }
            }
        }
    }
}
