using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sine : MonoBehaviour, IFunction
{
    [SerializeField]
    private float amplitude;
    [Range(0, Mathf.PI * 2f)]
    [SerializeField]
    private float phase;
    [SerializeField]
    private float frequency;

    public float Derivative(float x)
    {
        float fp = frequency * Mathf.PI * 2;
        return amplitude * fp * Mathf.Cos(fp * x + phase);
    }

    public float Function(float x)
    {
        float fp = frequency * Mathf.PI * 2;
        return amplitude * Mathf.Sin(fp * x + phase);
    }
}
