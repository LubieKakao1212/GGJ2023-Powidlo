using FMODUnity;
using UnityEngine;

public class MeleeUnit : Unit
{
    [SerializeField]
    private TickingExplosion explosion;

    [Tooltip("Multiplicative")]
    [SerializeField, Range(0, 1)] private float attackMovePenalty = 0.5f;
    
    private bool _didAttackLastTurn;

    public override void DoAction(Vector2 worldCursor)
    {
        RuntimeManager.PlayOneShot("event:/SFX/ElectricExplosion", GetComponent<Transform>().position);
        //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/ElectricExplosionINIT", GetComponent<Transform>().position);

        var expl = Instantiate(explosion, transform.position, Quaternion.identity);
        expl.playerId = playerId;
        _didAttackLastTurn = true;
    }

    public override void OnTurnStart() {
        base.OnTurnStart();
        if (_didAttackLastTurn) {
            _didAttackLastTurn = false;
            MovementLeftThisTurn *= attackMovePenalty;
        }
    }

    public override void OnMove(Vector3 delta) {
        base.OnMove(delta);
        RuntimeManager.PlayOneShot("event:/Characters/VacuumMove", GetComponent<Transform>().position);
    }

    public override void OnSelected()
    {
        base.OnSelected();

        RuntimeManager.PlayOneShot("event:/Characters/VacuumSelect", GetComponent<Transform>().position);
    }
}
