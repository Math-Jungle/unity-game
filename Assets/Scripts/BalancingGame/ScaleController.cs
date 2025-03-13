using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleController : MonoBehaviour
{
    public GameObject beam;
    public GameObject leftPan;
    public GameObject rightPan;
    public GameObject applePrefab;

    // Weight tracking
    private float leftWeight = 0f;
    private float rightWeight = 0f;

    // Reference to the beam's rigidbody
    private Rigidbody2D beamRb;

    // Lists to track apples on each side
    private List<GameObject> leftApples = new List<GameObject>();
    private List<GameObject> rightApples = new List<GameObject>();

    // Tolerance for balance
    public float balanceTolerance = 0.1f;

    void Start()
    {
        // Get beam's rigidbody
        beamRb = beam.GetComponent<Rigidbody2D>();

        // Set up hinge joint
        HingeJoint2D hinge = beam.GetComponent<HingeJoint2D>();
        if (hinge != null)
        {
            // Create anchor point
            GameObject anchor = new GameObject("Anchor");
            anchor.transform.position = beam.transform.position;
            anchor.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            hinge.connectedBody = anchor.GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        // Check if scale is balanced
        CheckBalance();

        // Add apples with mouse click (for testing)
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            // Determine which side to add the apple to
            if (mousePos.x < 0)
                AddAppleToSide(true);
            else
                AddAppleToSide(false);
        }
    }

    // Add apple to specified side (true for left, false for right)
    public void AddAppleToSide(bool isLeft)
    {
        GameObject newApple = Instantiate(applePrefab);
        Rigidbody2D appleRb = newApple.GetComponent<Rigidbody2D>();

        if (isLeft)
        {
            // Position apple above left pan
            newApple.transform.position = leftPan.transform.position + new Vector3(0, 2, 0);
            leftApples.Add(newApple);
            leftWeight += appleRb.mass;
        }
        else
        {
            // Position apple above right pan
            newApple.transform.position = rightPan.transform.position + new Vector3(0, 2, 0);
            rightApples.Add(newApple);
            rightWeight += appleRb.mass;
        }
    }

    // Check if scale is balanced
    private void CheckBalance()
    {
        float difference = Mathf.Abs(leftWeight - rightWeight);

        if (difference <= balanceTolerance)
        {
            Debug.Log("Scale is balanced!");
            // You can trigger win condition or next level here
        }
    }

    // Removes an apple from a side
    public void RemoveAppleFromSide(bool isLeft)
    {
        if (isLeft && leftApples.Count > 0)
        {
            GameObject apple = leftApples[leftApples.Count - 1];
            leftWeight -= apple.GetComponent<Rigidbody2D>().mass;
            leftApples.RemoveAt(leftApples.Count - 1);
            Destroy(apple);
        }
        else if (!isLeft && rightApples.Count > 0)
        {
            GameObject apple = rightApples[rightApples.Count - 1];
            rightWeight -= apple.GetComponent<Rigidbody2D>().mass;
            rightApples.RemoveAt(rightApples.Count - 1);
            Destroy(apple);
        }
    }
}