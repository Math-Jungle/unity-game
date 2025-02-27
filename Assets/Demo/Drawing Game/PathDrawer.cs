using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    public PathGame path;
    private LineRenderer myLineRenderer;

    public void CreatePath()
    {
        path = new PathGame(transform.position);
        myLineRenderer = this.AddComponent<LineRenderer>();
        myLineRenderer.widthMultiplier = 0.2f;
    }

    public void DrawPath(List<Vector2> points)
    {
        this.GetComponent<LineRenderer>().positionCount = points.Count;
        for (int i = 0; i < points.Count; i++)
        {
            this.GetComponent<LineRenderer>().SetPosition(i, points[i]);
        }
    }
}
