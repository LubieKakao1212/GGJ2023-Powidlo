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

    [SerializeField]
    private Material ChargedMaterial;

    [SerializeField]
    private Material DischargedMaterial;

    [SerializeField]
    private MeshRenderer display;

    [SerializeField]
    private int materialIndex;

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
        }
        discharged = true;

        var mats = display.materials;
        mats[materialIndex] = DischargedMaterial;
        display.materials = mats;
        
        charge = 0;
    }

    private void Recharge()
    {
        if (!discharged)
        {
            return;
        }

        if (++charge >= capacitance)
        {
            discharged = false; 
            
            var mats = display.materials;
            mats[materialIndex] = ChargedMaterial;
            display.materials = mats;

        }
    }
}
