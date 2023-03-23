using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public Material SelectedMateria { get; private set; }

    [field: SerializeField]
    public Material NormalMaterial { get; private set; }

    [SerializeField]
    private Rect movementBound;

    [SerializeField]
    private Rect slowedMovementBound;

    [SerializeField]
    private MeshRenderer rend;

    [field: SerializeField]
    public int health { get; private set; } = 10;

    [SerializeField]
    private float collisionSize;

    [SerializeField]
    protected bool isSlowed;

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
        var targetPos = transform.position + delta;

        var hits = Physics.OverlapSphere(targetPos, collisionSize);

        foreach (var hit in hits)
        {
            if ((hit.GetComponent<Unit>() != null || hit.GetComponent<Obstacle>() != null) && hit != GetComponent<Collider>())
            {
                //Put sound here
                return false;
            }
        }

        if (AlreadyMoved)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/NoAction", GetComponent<Transform>().position);

            Debug.LogWarning("Cannot move unit that already moved");
            return false;
        }

        

        transform.position += delta;
        AlreadyMoved = true;
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
        if (TurnManager.Instance.CurrentPlayer == Owner)
        {
            TurnManager.TurnPasses -= SkipTurn;
        }
    }

    public virtual void OnSelected()
    {
        
    }

    public virtual Rect GetMovementBounds()
    {
        return isSlowed ? slowedMovementBound : movementBound;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, collisionSize);
    }
}
