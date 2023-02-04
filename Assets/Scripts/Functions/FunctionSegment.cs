using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionSegment : MonoBehaviour
{
    [SerializeField]
    private LineRenderer curveLine;

    [SerializeField]
    public FunctionController Controller;

    [SerializeField]
    Rect bounds;

    [SerializeField]
    float minStep;

    public void Bake(Rect boundsIn)
    {
        IFunction func = Controller.GetFunction();

        List<Vector3> points = new List<Vector3>();

        Vector2 min = Vector2.Max(bounds.min, boundsIn.min);
        Vector2 max = Vector2.Min(bounds.max, boundsIn.max);

        float x = min.x;

        while (x < max.x)
        {
            float y = func.Function(x);

            if (!(y < min.y || y > max.y))
            {
                points.Add(new Vector3(x, 0, y));
            }
            //points.Add(new Vector3(x, 0, y));
            float slope = func.Derivative(x);
            float step = Mathf.Cos(Mathf.Atan(slope)) / Controller.pointDensity;
            //point density is one divided by distance between points, so instead of multiplying we divide
            x += Mathf.Max(step, minStep);
        }

        curveLine.useWorldSpace = false;
        curveLine.positionCount = points.Count;
        curveLine.SetPositions(points.ToArray());
    }
}
