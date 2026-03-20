using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{
    public event Action SelectedUnitChanged;

    public Unit CurrentUnit => units[_unit % units.Count];

    [SerializeField]
    private List<Unit> units;

    private int _unit = 0;

    private bool _isEnabled;

    private void Start()
    {
        TurnManager.TurnPasses += ResetUnitUseState;
    }

    public void DisabeControl()
    {
        _isEnabled = false;
        InputManager.SelectNextSecondary -= NextUnit;
        InputManager.SelectPreviousSecondary -= PreviousUnit;
        InputManager.PrimaryAction -= DoPrimaryAction;
    }

    public void EnableControl(bool newTurn)
    {
        if (newTurn) {
            foreach (var unit in units) {
                unit.OnTurnStart();
            }
        }
        if (!_isEnabled)
        {
            _isEnabled = true;
            InputManager.SelectNextSecondary += NextUnit;
            InputManager.SelectPreviousSecondary += PreviousUnit;
            InputManager.PrimaryAction += DoPrimaryAction;
        }
    }

    public void NextUnit()
    {
        SelectUnit((_unit + 1) % units.Count);
    }

    public void PreviousUnit()
    {
        var u = _unit--;
        if (u < 0)
        {
            u = units.Count - 1;
        }
        SelectUnit(u);
    }

    public void DoPrimaryAction()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.MousePos);
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1 << 7);

        if (hit.collider != null)
        {
            Unit unit = hit.collider.GetComponent<Unit>();
            if(unit.Owner == this) {
                var u = units.IndexOf(unit);
                if (u < 0)
                {
                    Debug.LogException(new IndexOutOfRangeException("Unit not found for player"), unit);
                    return;
                }
                SelectUnit(u);
            }
        }
    }

    public void DoUnitAction()
    {
        var unit = CurrentUnit;
        if (unit.AlreadyMoved)
        {
            RuntimeManager.PlayOneShot("event:/UI/NoAction", GetComponent<Transform>().position);
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(InputManager.MousePos);

        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1 << 8);

        //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/LaserShot", GetComponent<Transform>().position);

        unit.DoAction(new Vector2(hit.point.x, hit.point.z));

        unit.AlreadyMoved = true;
    }

    public void KillUnit(Unit unit)
    {
        if (units.Contains(unit))
        {
            bool flag = false;
            int i = units.IndexOf(unit);
            if (i == (units.Count - 1))
            {
                this._unit = -1;
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

    private void SelectUnit(int newUnit)
    {
        if (units.Count == 0)
        {
            TurnManager.Instance.RemovePlayer(this);
            return;
        }

        if (newUnit < 0 || newUnit > units.Count)
        {
            Debug.LogException(new IndexOutOfRangeException("Invelid unit index"), this);
            return;
        }

        _unit = newUnit;

        CurrentUnit.OnSelected();

        SelectedUnitChanged?.Invoke();
    }

    private void ResetUnitUseState()
    {
        foreach (var unit in units)
        {
            unit.AlreadyMoved = false;
        }
    }
}
