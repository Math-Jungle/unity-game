using UnityEngine;

public class Apple : MonoBehaviour{
    private bool isFalling = false;
    private Rigidbody2D rb;
    private GameManager gameManager;
    public GameObject collectEffect;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;
        rb.isKinematic = true;
    }

    void OnMouseDown(){
        if(!isFalling){
            StartFalling();
        }
    }

    void StartFalling(){
        isFalling = true;
        rb.isKinematic = false;
    }

    void OnCollisionEnter2D(Collision2D collison){
        if(collison.gameObject.CompareTag("Player") && isFalling){
            CollectApple();
        }
        else if(collison.gameObject.CompareTag("Ground") && isFalling){
            CollectApple();
        }
    }

    void CollectApple(){
        if(collectEffect != null){
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }
        gameManager.CollectApple();
        Destroy(gameObject);
    }
}