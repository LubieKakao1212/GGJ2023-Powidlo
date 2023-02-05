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

    private bool initialising;

    protected override void Start()
    {
        base.Start();
        Vector3 s = transform.localScale;
        transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, s, 0.5f).setEaseOutBack().setOvershoot(0.5f);
        initialising = true;
        if(damageOnStartup) {
            Explode();
        }
        initialising = false;
    }

    protected override bool Explode()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, size / 2f);

        foreach (var obj in objects)
        {
            DealDamage(obj, playerId, damage, isRanged);
        }

        if (!initialising)
        {
            LeanTween.scale(gameObject, Vector3.zero, 0.5f).setOnComplete(() => Destroy(gameObject)).setEaseInBack().setOvershoot(0.5f);
        }
        return true;
    }
}
