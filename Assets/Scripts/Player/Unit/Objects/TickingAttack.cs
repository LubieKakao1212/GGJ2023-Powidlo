using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TickingAttack : Ticking
{
    [SerializeField]
    public int playerId;

    [SerializeField]
    public int damage;

    protected void DealDamage(Unit unit)
    {
        if (unit != null && playerId != unit.playerId)
        {
            unit.Damage(damage);
        }
    }
}