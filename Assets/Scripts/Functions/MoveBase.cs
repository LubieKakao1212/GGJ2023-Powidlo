using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveBase : MonoBehaviour
{
    [SerializeField]
    protected Unit target;

    private bool isEnabled;

    public virtual void SetTarget(Unit target)
    {
        this.target = target;
    }

    public void DisabeControl()
    {
        InputManager.PointerPositionChanged -= UpdatePointer;
        InputManager.SecondaryAction -= Perform;
        isEnabled = false;
    }

    public void EnableControl()
    {
        if (!isEnabled)
        {
            InputManager.PointerPositionChanged += UpdatePointer;
            InputManager.SecondaryAction += Perform;
            isEnabled = true;
        }
    }

    private void OnEnable()
    {
        EnableControl();
    }

    private void OnDisable()
    {
        DisabeControl();
    }

    protected abstract void UpdatePointer(Vector2 pos);

    protected abstract void Perform();
}
