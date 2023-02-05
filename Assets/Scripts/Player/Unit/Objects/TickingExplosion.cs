using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static DamageUtil;

public class TickingExplosion : TickingAttack
{
    [SerializeField]
    private float size;

    [SerializeField]
    private bool isRanged;

    [SerializeField]
    private bool damageOnStartup;

    protected override void Start()
    {
        base.Start();
        if(damageOnStartup) {
            Explode();
        }
    }

    protected override void Explode()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, size / 2f);

        foreach (var obj in objects)
        {
            DealDamage(obj, playerId, damage, isRanged);
        }
    }
}
