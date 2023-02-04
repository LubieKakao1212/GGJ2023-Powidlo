using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action SelectedUnitChanged;

    public Unit CurrentUnit => units[unit];

    [SerializeField]
    private List<Unit> units;

    private int unit = 0;

    private bool isEnabled;

    private void Start()
    {
        TurnManager.TurnPasses += ResetUnitUseState;
    }

    public void DisabeControl()
    {
        isEnabled = false;
        InputManager.SelectNextSecondary -= NextUnit;
        InputManager.SelectPreviousSecondary -= PreviousUnit;
        InputManager.SecondaryAction -= DoUnitAction;
    }

    public void EnableControl()
    {
        if (!isEnabled)
        {
            isEnabled = true;
            InputManager.SelectNextSecondary += NextUnit;
            InputManager.SelectPreviousSecondary += PreviousUnit;
            InputManager.SecondaryAction += DoUnitAction;
        }
    }

    public void NextUnit()
    {
        int previous = unit++;
        unit = unit % units.Count;

        SelectUnit(previous);
    }

    public void PreviousUnit()
    {
        int previous = unit--;

        if (unit < 0)
        {
            unit = units.Count - 1;
        }

        SelectUnit(previous);
    }

    public void DoUnitAction()
    {
        var unit = CurrentUnit;
        if (unit.AlreadyUsedAction)
        {
            //Todo play sountd
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(InputManager.MousePos);

        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1 << 8);

        unit.DoAction(hit.point);
        unit.AlreadyUsedAction = true;
    }

    public void KillUnit(Unit unit)
    {
        if (units.Contains(unit))
        {
            bool flag = false;
            int i = units.IndexOf(unit);
            if (i == (units.Count - 1))
            {
                this.unit = -1;
                flag = true;
            }
            units.RemoveAt(i);

            if (flag)
            {
                NextUnit();
            }

            Destroy(unit.gameObject);
        }
    }

    private void SelectUnit(int previous)
    {
        var cUnit = CurrentUnit;
        var pUnit = units[previous];

        pUnit.GetComponent<MeshRenderer>().material = pUnit.NormalMaterial;
        cUnit.GetComponent<MeshRenderer>().material = cUnit.SelectedMateria;

        SelectedUnitChanged?.Invoke();
    }

    private void ResetUnitUseState()
    {
        foreach (var unit in units)
        {
            unit.AlreadyMoved = false;
            unit.AlreadyUsedAction = false;
        }
    }
}
