using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickingExplosion : MonoBehaviour
{
    [SerializeField]
    public int playerId;

    [SerializeField]
    float size;

    [SerializeField]
    private int timer = 2;

    private void Start()
    {
        TurnManager.TurnPasses += Tick;
    }

    private void Tick()
    {
        if(--timer <= 0)
        {
            Explode();
        }
        
    }

    private void Explode()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, size / 2f, 1 << 7);

        foreach (var obj in objects)
        {
            var unit = obj.GetComponent<Unit>();

            if (unit != null && playerId != unit.playerId)
            {
                unit.Owner.KillUnit(unit);
            }
        }

        TurnManager.TurnPasses -= Tick;
        Destroy(gameObject);
    }
}
