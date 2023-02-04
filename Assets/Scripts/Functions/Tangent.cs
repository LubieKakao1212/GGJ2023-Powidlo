using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tangent : MonoBehaviour, IFunction
{
    [SerializeField]
    private float a;

    public float Derivative(float x)
    {
        float c = Mathf.Cos(a * x);
        return a / (c* c);
    }

    public float Function(float x)
    {
        return Mathf.Tan(a * x);
    }
}
