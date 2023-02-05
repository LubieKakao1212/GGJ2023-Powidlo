using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnit : Unit
{
    [SerializeField]
    private TickingExplosion explosion;

    private int slowCounter;

    public override void DoAction(Vector2 worldCursor)
    {
        var expl = Instantiate(explosion, transform.position, Quaternion.identity);
        expl.playerId = playerId;

        this.isSlowed = true;
        slowCounter = 2;
        TurnManager.TurnPasses += DisableSlow;

        //SkipNextTurn();
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

    private void DisableSlow()
    {
        if (slowCounter-- <= 0)
        {
            this.isSlowed = false;
            TurnManager.TurnPasses -= DisableSlow;
        }
    }
}
