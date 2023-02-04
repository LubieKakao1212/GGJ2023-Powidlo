using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linear : MonoBehaviour, IFunction
{
    [SerializeField]
    private float a;

    public float Derivative(float x)
    {
        return x;
    }

    public float Function(float x)
    {
        return a * x;
    }
}
