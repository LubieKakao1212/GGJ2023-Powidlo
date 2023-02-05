using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public virtual void OnHit(int playerId, int damage, bool ranged) { }
}
