using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageUtil;

public class Laser : TickingAttack
{
    [SerializeField]
    private LineRenderer laserLine;

    [SerializeField]
    private float maxDistance;

    public void Prime(Ray ray)
    {
        Vector3 endPoint = ray.GetPoint(maxDistance);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            var col = hit.collider;
            endPoint = hit.point;

            DealDamage(col, playerId, damage, true);
        }
        laserLine.positionCount = 2;
        laserLine.SetPositions(new Vector3[] { ray.origin, endPoint });
    }

    protected override bool Explode()
    {
        //Intentional
        return false;
    }
}
