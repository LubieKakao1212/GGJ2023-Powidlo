using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MovementManager : MonoBehaviour
{
    [SerializeField]
    private MoveToFunction functionMover;

    private FunctionController currentFunction;

    private Dictionary<MovementFunctionType, FunctionController> functionsByType;

    private List<FunctionController> functionsByOrder;

    private bool isEnabled;

    public void DisabeControl()
    {
        functionMover.DisabeControl();

        InputManager.SelectNextPrimary -= NextFunction;
        InputManager.SelectPreviousPrimary -= PreviousFunction;

        currentFunction.gameObject.SetActive(false);

        isEnabled = false;
    }

    public void EnableControl()
    {
        if (!isEnabled)
        {
            functionMover.EnableControl();
            currentFunction.gameObject.SetActive(true);
            InputManager.SelectNextPrimary += NextFunction;
            InputManager.SelectPreviousPrimary += PreviousFunction;
            isEnabled = true;
        }
    }

    public void SetupCache()
    {
        functionsByType = new Dictionary<MovementFunctionType, FunctionController>();
        functionsByOrder = new List<FunctionController>();

        foreach (var function in GetComponentsInChildren<MovementFunction>(true))
        {
            var func = function.FunctionObj;
            Assert.IsNotNull(func);
            functionsByType.Add(function.Type, func);
            functionsByOrder.Add(func);
        }
    }

    public void SetCurrentFunction(FunctionController newCurrent)
    {
        if (isEnabled)
        {
            currentFunction.gameObject.SetActive(false);
        }

        if (!functionsByOrder.Contains(newCurrent))
        {
            Debug.LogWarning("Invalid Function");
            return;
        }
        currentFunction = newCurrent;
        functionMover.SetController(currentFunction);

        if (isEnabled)
        {
            currentFunction.gameObject.SetActive(true);
        }

        functionMover.UpdatePointer();
    }

    public void NextFunction()
    {
        var i = GetCurrentIndex();
        i = (++i) % functionsByOrder.Count;

        SetCurrentFunction(functionsByOrder[i]);
    }

    public void PreviousFunction()
    {
        var i = GetCurrentIndex();
        i--;

        if (i < 0)
        {
            i = functionsByOrder.Count - 1;
        }

        SetCurrentFunction(functionsByOrder[i]);
    }

    public void SetTarget(Unit target)
    {
        functionMover.SetTargetController(target, currentFunction);
    }

    private int GetCurrentIndex()
    {
        int i = 0;
        if (currentFunction != null)
        { 
            i = functionsByOrder.IndexOf(currentFunction);
        }
        return i;
    }
}

/// <summary>
/// '_' means negative
/// </summary>
public enum MovementFunctionType
{
    X,
    X_,
    X2,
    X2_,
    InvX,
    Sin
}
