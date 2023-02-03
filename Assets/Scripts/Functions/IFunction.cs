using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFunction
{
    float Function(float x);

    float Derivative(float x);
}