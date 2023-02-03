using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverse : MonoBehaviour, IFunction
{
    [SerializeField]
    private float a;

    public float Derivative(float x)
    {
        return -a / (x * x);
    }

    public float Function(float x)
    {
        return a / x;
    }
}