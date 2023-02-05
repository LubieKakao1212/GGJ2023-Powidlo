using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacitorObstacle : Obstacle
{
    [SerializeField]
    private TickingExplosion explosionPrefab;

    [SerializeField]
    private bool discharged;

    [SerializeField]
    private int capacitance;

    [SerializeField]
    public int charge;

    private void Start()
    {
        TurnManager.TurnPasses += Recharge;
    }

    public override void OnHit(int playerId, int damage, bool ranged)
    {
        if (!ranged)
        {
            return;
        }
        if (!discharged)
        {
            var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosion.playerId = int.MinValue;

            //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/ElectricExplosion", GetComponent<Transform>().position);
        }
        discharged = true;
        charge = 0;
    }

    private void Recharge()
    {
        if (!discharged)
        {
            return;
        }

        if (charge++ >= capacitance)
        {
            discharged = false;
        }
    }
}
