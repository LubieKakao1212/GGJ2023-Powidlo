using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MoveManager : MonoBehaviour
{
    [SerializeField]
    private MoveBase currentMove;

    [SerializeField]
    private List<MoveBase> movesByOrder;

    public void SetupCache()
    {
        movesByOrder = new List<MoveBase>();

        foreach (var function in GetComponentsInChildren<MoveBase>(true))
        {
            movesByOrder.Add(function);
        }
    }

    public void SetCurrentMove(MoveBase newCurrent)
    {
        var hasControll = !TurnManager.Instance.CurrentUnit.AlreadyMoved;
        UpdateIfHasControll(hasControll, false);

        if (!movesByOrder.Contains(newCurrent))
        {
            Debug.LogWarning("Invalid Function");
            return;
        }

        currentMove = newCurrent;
        if (currentMove != null)
        {
            UpdateIfHasControll(hasControll, true);
            currentMove.SetTarget(TurnManager.Instance.CurrentPlayer.CurrentUnit);
            //currentMove.UpdatePointer();
        }
    }

    public void UpdateIfHasControll(bool hasControll, bool newState)
    {
        if (hasControll && currentMove != null)
        {
            currentMove.gameObject.SetActive(newState);
        }
    }

    public void ClearMove()
    {
        UpdateIfHasControll(true, false);
        currentMove = null;
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
