using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipeUnit : Unit
{
    [SerializeField]
    private Laser laserPrefab;

    public override void DoAction(Vector2 worldCursor)
    {
        var laser = Instantiate(laserPrefab);
        laser.playerId = playerId;

        var dir = worldCursor.XYToXZ() - transform.position.XZToXY().XYToXZ();

        laser.Prime(new Ray(transform.position, dir));

        transform.rotation = Quaternion.LookRotation(dir.normalized, Vector3.up);
    }

    public override bool Move(Vector3 delta)
    {
        if (base.Move(delta))
        {
            return true;
        }
        return false;
    }

    public override void OnSelected()
    {
        base.OnSelected();
    }
}
