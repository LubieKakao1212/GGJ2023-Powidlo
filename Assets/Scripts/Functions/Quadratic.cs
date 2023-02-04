using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadratic : MonoBehaviour, IFunction
{
    [SerializeField]
    private float a;

    public float Derivative(float x)
    {
        return 2 * x * x;
    }

    public float Function(float x)
    {
        return x * a * x;
    }
}
