using FMODUnity;
using UnityEngine;

public class SnipeUnit : Unit
{
    [SerializeField]
    private Laser laserPrefab;

    public override void DoAction(Vector2 worldCursor)
    {
        RuntimeManager.PlayOneShot("event:/SFX/LaserShot", GetComponent<Transform>().position);

        var laser = Instantiate(laserPrefab);
        laser.playerId = playerId;

        var dir = worldCursor.XYToXZ() - transform.position.XZToXY().XYToXZ();

        laser.Prime(new Ray(transform.position, dir));

        transform.rotation = Quaternion.LookRotation(dir.normalized, Vector3.up);
    }

    public override void OnMove(Vector3 delta) {
        base.OnMove(delta);
        RuntimeManager.PlayOneShot("event:/Characters/MrLaserMove", GetComponent<Transform>().position);
    }

    public override void OnSelected()
    {
        base.OnSelected();

        RuntimeManager.PlayOneShot("event:/Characters/MrLaserSelect", GetComponent<Transform>().position);
    }
}
