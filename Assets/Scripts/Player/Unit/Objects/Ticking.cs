using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ticking : MonoBehaviour
{
    [SerializeField]
    private int timer = 2;

    protected virtual void Start()
    {
        TurnManager.TurnPasses += Tick;
    }

    private void Tick()
    {
        if (--timer <= 0)
        {
            if (!Explode())
            {
                Destroy(gameObject);
            }
            TurnManager.TurnPasses -= Tick;
        }
    }

    protected abstract bool Explode();
}
