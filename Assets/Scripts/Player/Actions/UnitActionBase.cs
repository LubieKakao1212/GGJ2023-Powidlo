using UnityEngine;

public abstract class UnitActionBase : MonoBehaviour
{
    [SerializeField]
    protected Unit unit;

    private bool isEnabled;

    public virtual void SetTarget(Unit target)
    {
        this.unit = target;
    }

    public virtual void DisabeControl()
    {
        InputManager.PointerPositionChanged -= UpdatePointer;
        InputManager.SecondaryAction -= Perform;
        isEnabled = false;
    }

    public virtual void EnableControl()
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
