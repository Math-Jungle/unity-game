using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[System.Serializable]
public class PathGame
{

    public List<Vector2> points;

    public PathGame(Vector2 center)
    {

        points = new List<Vector2>
        {
            center + Vector2.left,
            center + Vector2.left * 0.35f,
            center + Vector2.right * 0.35f,
            center + Vector2.right,
        };
    }
    public Vector2 this[int i] => points[i];

    public int Count => points.Count;

    public int NumPoints => points.Count;

    public int NumSegments => (points.Count - 4) / 3 + 1;

    public void AddSegment(Vector2 anchorPos)
    {
        points.Add(anchorPos);
    }

    public Vector2[] GetPointsInSegment(int i)
    {
        return new Vector2[]
        {
            points[i*3],
            points[i*3+1],
            points[i*3+2],
            points[i*3+3]
        };
    }
    public void MovePoint(int i, Vector2 pos) => points[i] = pos;



}
