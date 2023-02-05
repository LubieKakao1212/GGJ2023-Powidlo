using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipeUnit : Unit
{
    [SerializeField]
    private Laser laserPrefab;

    public override void DoAction(Vector2 worldCursor)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/LaserShot", GetComponent<Transform>().position);

        var laser = Instantiate(laserPrefab);
        laser.playerId = playerId;

        var dir = worldCursor.XYToXZ() - transform.position.XZToXY().XYToXZ();

        laser.Prime(new Ray(transform.position, dir));

        transform.rotation = Quaternion.LookRotation(dir.normalized, Vector3.up);
    }

    public override void Move(Vector3 delta)
    {
        base.Move(delta);
    }

    public override void OnSelected()
    {
        base.OnSelected();
    }
}
