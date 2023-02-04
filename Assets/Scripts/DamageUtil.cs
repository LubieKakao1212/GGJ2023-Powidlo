using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageUtil
{
    public static void DealDamage(Collider collider, int playerId, int damage, bool isRanged)
    {
        var unit = collider.GetComponent<Unit>();
        if (unit != null && playerId != unit.playerId)
        {
            unit.Damage(damage);
        }

        var obstacle = collider.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            obstacle.OnHit(playerId, damage, isRanged);
        }
    }
}
