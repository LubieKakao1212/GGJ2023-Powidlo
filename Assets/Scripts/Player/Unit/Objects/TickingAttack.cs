using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TickingAttack : Ticking
{
    [SerializeField]
    public int playerId;

    [SerializeField]
    public int damage;
}