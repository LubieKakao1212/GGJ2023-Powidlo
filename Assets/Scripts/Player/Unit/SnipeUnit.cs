using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipeUnit : Unit
{
    [SerializeField]
    private Laser laserPrefab;

    public override void DoAction(Vector3 worldCursor)
    {
        var laser = Instantiate(laserPrefab);
        laser.playerId = playerId;

        var dir = worldCursor - transform.position;

        laser.Prime(new Ray(transform.position, dir));

        transform.rotation = Quaternion.LookRotation(dir.normalized, Vector3.up);
    }
}
