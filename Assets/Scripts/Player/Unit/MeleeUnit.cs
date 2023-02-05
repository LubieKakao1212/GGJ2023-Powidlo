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

    public override void Move(Vector3 delta)
    {
        base.Move(delta);

        FMODUnity.RuntimeManager.PlayOneShot("event:/Characters/VacuumMove", GetComponent<Transform>().position);
    }

    public override void OnSelected()
    {
        base.OnSelected();

        FMODUnity.RuntimeManager.PlayOneShot("event:/Characters/VacuumSelect", GetComponent<Transform>().position);
    }
}
