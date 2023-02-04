using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : TickingAttack
{
    [SerializeField]
    private LineRenderer laserLine;

    [SerializeField]
    private float maxDistance;

    public void Prime(Ray ray)
    {
        Vector3 endPoint = ray.GetPoint(maxDistance);
        List<RaycastHit> hits = Physics.RaycastAll(ray, 10).ToList();
        hits.Sort((hit1, hit2) => (int)Mathf.Sign(hit2.distance - hit1.distance));
        foreach (var hit in hits)
        {
            Debug.Log(hit.distance);
            var col = hit.collider;
            if (col.tag == "LaserProof")
            {
                endPoint = hit.point;
                break;
            }

            var unit = col.GetComponent<Unit>();
            if (unit != null)
            {
                DealDamage(unit);
            }
        }

        laserLine.positionCount = 2;
        laserLine.SetPositions(new Vector3[] { ray.origin, endPoint });
    }

    protected override void Explode()
    {
        //Intentional
    }
}
