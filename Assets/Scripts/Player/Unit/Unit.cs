using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [field: SerializeField]
    public int playerId { get; private set; }

    [field: SerializeField]
    public Player Owner { get; private set; }
    
    [field: SerializeField]
    public bool AlreadyMoved { get; set; }

    [field: SerializeField]
    public bool AlreadyUsedAction { get; set; }

    [field: SerializeField]
    public Material SelectedMateria { get; private set; }

    [field: SerializeField]
    public Material NormalMaterial { get; private set; }

    public abstract void DoAction(Vector3 worldCursor);

    public void Move(Vector3 delta)
    {
        if (AlreadyMoved)
        {
            Debug.LogWarning("Cannot move unit that already moved");
            return;
        }
        transform.position += delta;
        AlreadyMoved = true;
    }
}
