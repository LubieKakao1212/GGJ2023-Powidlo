using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnit : Unit
{
    [SerializeField]
    private TickingExplosion explosion;

    public override void DoAction(Vector2 worldCursor)
    {
        var expl = Instantiate(explosion, transform.position, Quaternion.identity);
        expl.playerId = playerId;

        SkipNextTurn();
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
