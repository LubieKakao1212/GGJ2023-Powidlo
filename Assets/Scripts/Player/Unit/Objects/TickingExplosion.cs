using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickingExplosion : TickingAttack
{
    [SerializeField]
    float size;
    
    protected override void Explode()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, size / 2f, 1 << 7);

        foreach (var obj in objects)
        {
            var unit = obj.GetComponent<Unit>();

            DealDamage(unit);
        }
    }
}
