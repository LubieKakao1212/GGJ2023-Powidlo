using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ticking : MonoBehaviour
{
    [SerializeField]
    private int timer = 2;

    private void Start()
    {
        TurnManager.TurnPasses += Tick;
    }

    private void Tick()
    {
        if (--timer <= 0)
        {
            Explode();
            Destroy(gameObject);
            TurnManager.TurnPasses -= Tick;
        }
    }

    protected abstract void Explode();
}