using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

public class FunctionController : MonoBehaviour
{
    [SerializeField]
    private List<FunctionSegment> segments;

    [SerializeField]
    private LineRenderer displayControl;

    [SerializeField]
    private GameObject indicator;

    [SerializeField]
    private float distance;

    [field: SerializeField]
    public float pointDensity { get; private set; }

    [field: SerializeField]
    public Rect bounds { get; private set; }
    
    [SerializeField]
    private GameObject functionProvider;

    [SerializeField]
    private float controlUpdateAccuracy;
    [SerializeField]
    private float controlUpdateMinStep;

    [SerializeField]
    private float sensitivity;

    public Vector2? UpdateControl(Vector2 position)
    {
        IFunction func = GetFunction();

        //displayControl.positionCount = 2;

        float x = position.x;
        Vector2 pointOnCurve = new Vector2(x, func.Function(x));

        //Calculate closest point forward
        Vector2 p1 = GetClosest(func, pointOnCurve, position, 1);
        //Calculate closest point backward
        Vector2 p2 = GetClosest(func, pointOnCurve, position, -1);

        float m1 = (p1 - position).magnitude;
        float m2 = (p2 - position).magnitude;

        Vector2 p = m1 < m2 ? p1 : p2;

        //Vector3[] positions = new Vector3[] { position.XYToXZ(), p.XYToXZ() };

        //displayControl.SetPositions(positions);

        //Is the cursor approximatly on the curve
        if ((m1 < sensitivity || m2 < sensitivity) && bounds.Contains(p))
        {
            indicator.SetActive(true);
            indicator.transform.localPosition = p.XYToXZ();
            return p;
        }
        else
        {
            indicator.SetActive(false);
        }

        return null;
    }

    [ContextMenu("Generate")]
    public void Generate()
    {
        foreach (var segment in segments)
        {
            segment.Controller = this;
            segment.Bake(bounds);
        }
    }

    private Vector2 GetClosest(IFunction func, Vector2 pointOnCurve, Vector2 position, float direction)
    {
        float distance = Mathf.Infinity;
        float x = position.x;
        Vector2 closestPoint = Vector2.zero;
        //iterate over 4096 iterations
        for (int i = 0; i < 10000; i++)
        {
            float dist = (pointOnCurve - position).magnitude;

            if (dist < distance)
            {
                distance = dist;
                closestPoint = pointOnCurve;
            }

            float d = func.Derivative(x) * controlUpdateAccuracy;
            d = Mathf.Max(Mathf.Abs(d), controlUpdateMinStep) * direction;
            x += d;

            pointOnCurve.x = x;
            pointOnCurve.y = func.Function(x);
        }

        return closestPoint;
    }

    private void Start()
    {
        Generate();
        //displayControl.positionCount = 2;
    }

    

    public IFunction GetFunction()
    {
        IFunction func = functionProvider.GetComponent<IFunction>();

        Assert.IsNotNull((MonoBehaviour)func);
        return func;
    }
}
