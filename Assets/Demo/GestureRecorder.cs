using System.Collections.Generic;
using PDollarGestureRecognizer;
using TMPro;
using UnityEngine;

public class GestureRecorder : MonoBehaviour
{
    [Header("References")]
    public LineRenderer lineRenderer;
    public string gestureName;  // Name to save the gesture as

    private List<Point> points = new List<Point>();
    private int strokeId = 0;
    private bool isRecording = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    StartRecording(touch.position);
                    break;

                case TouchPhase.Moved:
                    if (isRecording) ContinueRecording(touch.position);
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (isRecording) StopRecording();
                    break;
            }
        }
    }

    private void StartRecording(Vector2 screenPosition)
    {
        isRecording = true;
        points.Clear();
        strokeId++;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10f));
        points.Add(new Point(worldPosition.x, worldPosition.y, strokeId));

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, worldPosition);
    }

    private void ContinueRecording(Vector2 screenPosition)
    {
        if (!isRecording) return;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10f));
        points.Add(new Point(worldPosition.x, worldPosition.y, strokeId));

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, worldPosition);
    }

    private void StopRecording()
    {
        isRecording = false;

        if (points.Count < 10)
        {
            Debug.LogWarning("Gesture too short. Discarding.");
            return;
        }

        if (string.IsNullOrEmpty(gestureName))
        {
            Debug.LogWarning("Please enter a name for the gesture.");
            return;
        }

        // Save the gesture
        SaveGesture(points, gestureName);
        Debug.Log($"Gesture '{gestureName}' saved with {points.Count} points.");

        // Reset the line renderer
        lineRenderer.positionCount = 0;
        gestureName = string.Empty;

    }

    private void SaveGesture(List<Point> points, string gestureName)
    {
        Point[] pointsArray = points.ToArray();

        // Save the gesture using PDollarGestureRecognizer's GestureIO class
        // Ensure the directory exists
        string directoryPath = "Assets/Resources/Gestures/";

        if (!System.IO.Directory.Exists(directoryPath))
        {
            System.IO.Directory.CreateDirectory(directoryPath);
        }

        GestureIO.WriteGesture(pointsArray, gestureName, directoryPath + gestureName + ".xml");
    }
}
