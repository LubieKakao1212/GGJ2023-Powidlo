using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveManager : MonoBehaviour
{
    [FormerlySerializedAs("currentMove")] [SerializeField]
    private UnitActionBase currentAction;

    [SerializeField]
    private List<UnitActionBase> movesByOrder;

    public void SetupCache() {
        movesByOrder = new List<UnitActionBase>();

        foreach (var function in GetComponentsInChildren<UnitActionBase>(true))
        {
            movesByOrder.Add(function);
        }

        InputManager.SelectNumber += SetCurrentMove;
    }

    public void SetCurrentMove(int id) {
        if (id == 0) {
            id = 10;
        }
        if (--id < movesByOrder.Count) {
            SetCurrentMove(movesByOrder[id]);
        }
    }
    
    public void SetCurrentMove(UnitActionBase newCurrent)
    {
        var hasControll = !TurnManager.Instance.CurrentUnit.AlreadyMoved;
        UpdateIfHasControll(hasControll, false);

        if (!movesByOrder.Contains(newCurrent))
        {
            Debug.LogWarning("Invalid Function");
            return;
        }

        currentAction = newCurrent;
        if (currentAction != null)
        {
            currentAction.SetTarget(TurnManager.Instance.CurrentPlayer.CurrentUnit);
            UpdateIfHasControll(hasControll, true);
            //currentMove.UpdatePointer();
        }
    }

    public void UpdateIfHasControll(bool hasControll, bool newState)
    {
        if (hasControll && currentAction != null)
        {
            currentAction.gameObject.SetActive(newState);
        }
    }

    public void ClearMove()
    {
        UpdateIfHasControll(true, false);
        currentAction = null;
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
