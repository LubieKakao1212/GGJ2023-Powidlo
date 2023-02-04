using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [field: SerializeField]
    public int playerId { get; private set; }

    [field: SerializeField]
    public Player Owner { get; private set; }

    public abstract void DoAction(Vector3 worldCursor);
}
