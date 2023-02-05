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
