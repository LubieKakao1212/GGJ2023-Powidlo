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
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/ElectricExplosion", GetComponent<Transform>().position);
        //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/ElectricExplosionINIT", GetComponent<Transform>().position);

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
            FMODUnity.RuntimeManager.PlayOneShot("event:/Characters/VacuumMove", GetComponent<Transform>().position);

            return true;
        }
        return false;

    }

    public override void OnSelected()
    {
        base.OnSelected();

        FMODUnity.RuntimeManager.PlayOneShot("event:/Characters/VacuumSelect", GetComponent<Transform>().position);
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
