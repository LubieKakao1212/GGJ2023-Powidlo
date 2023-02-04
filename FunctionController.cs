using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionController : MonoBehaviour
{
    [SerializeField]
    private LineRenderer functionLine;
    
    [SerializeField]
    private LineRenderer displayControl;

    [SerializeField]
    private float distance;

    [SerializeField]
    private float pointDensity;

    [SerializeField]
    private Vector2 bounds;

    [SerializeField]
    private GameObject functionProvider;

    public Vector3 UpdatControl(Vector2 position)
    {
        IFunction func = GetFunction();

        displayControl.positionCount = 2;

        Vector2 pointOnCurve = new Vector2(position.x, func.Function(position.x));

        Vector3[] positions = new Vector3[] { position, pointOnCurve };

        displayControl.SetPositions(positions);
        return pointOnCurve;
    }

    private void Start()
    {
        Generate();
    }

    [ContextMenu("Generate")]
    private void Generate()
    {
        IFunction func = GetFunction();

        List<Vector3> points = new List<Vector3>();

        float x = -bounds.x;

        while (x < bounds.x)
        {
            float y = func.Function(x);

            if (Mathf.Abs(y) < bounds.y)
            {
                points.Add(new Vector3(x, 0, y));
            }
            float slope = func.Derivative(x);
            //point density is one divided by distance between points, so instead of multiplying we divide
            x += Mathf.Cos(Mathf.Atan(slope)) / pointDensity;
        }

        functionLine.useWorldSpace = false;
        functionLine.positionCount = points.Count;
        functionLine.SetPositions(points.ToArray());
    }

    private IFunction GetFunction()
    {
        IFunction func = functionProvider.GetComponent<IFunction>();

        if (((MonoBehaviour)func) == null)
        {
            Debug.LogError("Invalid function definition");
            return null;
        }
        return func;
    }
}
