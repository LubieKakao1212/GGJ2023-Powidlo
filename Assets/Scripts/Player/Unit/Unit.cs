using System;
using FMODUnity;
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

    public float MovementLeftThisTurn { get; protected set; }

    [SerializeField] private float movementPerTurn = 5f;

    [SerializeField]
    private MeshRenderer rend;

    [field: SerializeField]
    public int health { get; private set; } = 10;

    [SerializeField]
    private float collisionSize;

    public void Damage(int amount)
    {
        health -= amount;

        Damaged?.Invoke(health);

        if (health <= 0)
        {
            Owner.KillUnit(this);
        }
    }

    public virtual void OnTurnStart() {
        MovementLeftThisTurn = movementPerTurn;
    }
    
    public abstract void DoAction(Vector2 worldCursor);

    public virtual void OnMove(Vector3 delta) {
        MovementLeftThisTurn -= delta.magnitude;
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
