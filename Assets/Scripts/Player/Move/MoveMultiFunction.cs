using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMultiFunction : MoveToFunction
{
    [SerializeField]
    private FunctionController[] functions;

    private int index;

    public override void EnableControl()
    {
        NextFunction();
        base.EnableControl();
    }

    public void NextFunction()
    {
        if (controller != null)
        {
            controller.gameObject.SetActive(false);
        }
        controller = functions[index];
        controller.gameObject.SetActive(true);
        index = (++index) % functions.Length;
    }
}
