using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementFunction : MonoBehaviour
{
    [field: SerializeField]
    public MovementFunctionType Type { get; private set; }

    [field: SerializeField]
    public FunctionController FunctionObj;
}
