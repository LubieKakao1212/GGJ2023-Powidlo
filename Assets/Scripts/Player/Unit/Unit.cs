using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    /// <summary>
    /// arg - health left
    /// </summary>
    public event Action<int> Damaged;

    [field: SerializeField]
    public int playerId { get; private set; }

    [field: SerializeField]
    public Player Owner { get; private set; }

    [field: SerializeField]
    public bool AlreadyMoved { get; set; }

    [field: SerializeField]
    public bool AlreadyUsedAction { get; set; }

    [field: SerializeField]
    public Material SelectedMateria { get; private set; }

    [field: SerializeField]
    public Material NormalMaterial { get; private set; }

    [SerializeField]
    private MeshRenderer rend;

    [field: SerializeField]
    public int health { get; private set; } = 10;
    
    public void Damage(int amount)
    {
        health -= amount;

        Damaged?.Invoke(health);

        if (health <= 0)
        {
            Owner.KillUnit(this);
        }
    }

    public abstract void DoAction(Vector2 worldCursor);

    public virtual bool Move(Vector3 delta)
    {
        if (AlreadyMoved)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/NoAction", GetComponent<Transform>().position);

            Debug.LogWarning("Cannot move unit that already moved");
            return false;
        }

        

        transform.position += delta;
        AlreadyMoved = true;
        AlreadyUsedAction = true;
        return true;
    }

    public void SwitchMaterial(Material target)
    {
        rend.material = target;
    }

    public void SkipNextTurn()
    {
        TurnManager.TurnPasses += SkipTurn;
    }

    public void SkipTurn()
    {
        AlreadyMoved = true;
        AlreadyUsedAction = true;
        if (TurnManager.Instance.CurrentPlayer == Owner)
        {
            TurnManager.TurnPasses -= SkipTurn;
        }
    }

    public virtual void OnSelected()
    {
        
    }

    private void Start()
    {
        Owner.SelectedUnitChanged += AdjustMaterial;
    }

    private void OnDestroy()
    {
        Owner.SelectedUnitChanged -= AdjustMaterial;
    }

    private void AdjustMaterial()
    {
        if (Owner.CurrentUnit != this)
        {
            SwitchMaterial(NormalMaterial);
        }
        else
        {
            SwitchMaterial(SelectedMateria);
        }
    }
}
