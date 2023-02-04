using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action SelectedUnitChanged;

    public Unit CurrentUnit => units[unit];

    [SerializeField]
    private List<Unit> units;

    private int unit = 0;

    private bool isEnabled;

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
        unit = (++unit) % units.Count;
        SelectedUnitChanged?.Invoke();
    }

    public void PreviousUnit()
    {
        unit--;

        if (unit < 0)
        {
            unit = units.Count - 1;
        }

        SelectedUnitChanged?.Invoke();
    }

    public void DoUnitAction()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.MousePos);

        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1 << 8);

        CurrentUnit.DoAction(hit.point);
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
}
