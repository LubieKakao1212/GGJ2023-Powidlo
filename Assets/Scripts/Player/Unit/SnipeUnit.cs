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

        laser.Prime(new Ray(transform.position, worldCursor - transform.position));
    }
}
