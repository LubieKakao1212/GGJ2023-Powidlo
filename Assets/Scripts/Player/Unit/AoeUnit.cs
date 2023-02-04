using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeUnit : Unit
{
    [SerializeField]
    private TickingExplosion explosion;

    public override void DoAction(Vector3 worldCursor)
    {
        var expl = Instantiate(explosion, transform.position, Quaternion.identity);
        expl.playerId = playerId;
    }
}
