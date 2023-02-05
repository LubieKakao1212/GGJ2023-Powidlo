using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnit : Unit
{
    [SerializeField]
    private TickingExplosion explosion;

    public override void DoAction(Vector2 worldCursor)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/ElectricExplosion", GetComponent<Transform>().position);

        var expl = Instantiate(explosion, transform.position, Quaternion.identity);
        expl.playerId = playerId;

        SkipNextTurn();
    }
}
