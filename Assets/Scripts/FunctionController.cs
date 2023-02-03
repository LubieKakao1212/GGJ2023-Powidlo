using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

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

    [SerializeField]
    private float controlUpdateAccuracy;

    public Vector3 UpdateControl(Vector2 position)
    {
        IFunction func = GetFunction();

        displayControl.positionCount = 2;
        
        float x = position.x;
        bool isAbove = func.Function(x) < position.y;
        Vector2 pointOnCurve = new Vector2(position.x, func.Function(x));

        float lastDistance = Mathf.Infinity;
        
        //Hard limit of 10000 iterations
        for (int i = 0; i < 10000; i++) 
        {
            float distance = (pointOnCurve - position).magnitude;
            bool isAbove2 = func.Function(x) < position.y;
            if (lastDistance < distance || isAbove2 != isAbove)
            {
                break;
            }
            lastDistance = distance;
            float d = func.Derivative(x) * controlUpdateAccuracy;
            if (!isAbove)
            {
                d *= -1;
            }
            x += d;

            pointOnCurve.x = x;
            pointOnCurve.y = func.Function(x);
        }

        Vector3[] positions = new Vector3[] { position.XYToXZ(), pointOnCurve.XYToXZ() };

        displayControl.SetPositions(positions);

        return pointOnCurve;
    }

    private void Start()
    {
        Generate();
        displayControl.positionCount = 2;
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

        Assert.IsNotNull((MonoBehaviour)func);
        return func;
    }
}
