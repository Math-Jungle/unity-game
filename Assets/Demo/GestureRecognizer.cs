using System.Collections.Generic;
using PDollarGestureRecognizer;
using UnityEngine;
using UnityEngine.UIElements;

public class GestureRecognizer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    private List<Point> points = new List<Point>();
    private int stokeId = 0;
    private bool isDrawing = false;
    [SerializeField] private float lineWidth = 0.1f;

    // Predifined number templates (0-9)
    private List<Gesture> trainingSet = new List<Gesture>();

    void Start()
    {
        // Load gesture templates
        LoadGestureTemplates();
    }

    void Update()
    {
        // Handle touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    StartDrawing(touch.position);
                    break;

                case TouchPhase.Moved:
                    if (isDrawing) ContinueDrawing(touch.position);
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (isDrawing) StopDrawing();
                    break;
            }
        }
    }


    void StartDrawing(Vector2 position)
    {
        isDrawing = true;
        points.Clear();
        stokeId++;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 10f));
        points.Add(new Point(worldPosition.x, worldPosition.y, stokeId));

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, worldPosition);
    }

    void ContinueDrawing(Vector2 position)
    {
        if (!isDrawing) return;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 10f));
        points.Add(new Point(worldPosition.x, worldPosition.y, stokeId));

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, worldPosition);
    }

    void StopDrawing()
    {
        isDrawing = false;

        // Recognizing the drawn shape
        Gesture result = new Gesture(points.ToArray());
        Result recognitionResult = PointCloudRecognizer.Classify(result, trainingSet.ToArray());
        string recognizedNumber = recognitionResult.GestureClass;

        Debug.Log("You drew: " + recognizedNumber);

        // Checking if correct number is drawn
        if (recognizedNumber == "0")
        {
            Debug.Log("Correct number drawn!");
        }
        else
        {
            Debug.Log("Incorrect number drawn. Try again.");
            lineRenderer.positionCount = 0; // Reset the line renderer
        }
    }

    void LoadGestureTemplates()
    {
        // Load gesture templates from resources folder
        TextAsset[] gestureFiles = Resources.LoadAll<TextAsset>("GestureSet");
        Debug.Log($"Found {gestureFiles.Length} gesture files in Resources/Gestures folder");
        foreach (TextAsset gestureXml in gestureFiles)
        {
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
        }
        Debug.Log("Loaded " + trainingSet.Count + " gesture templates.");
    }

}
