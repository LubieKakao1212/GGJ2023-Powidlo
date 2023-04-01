using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAttack : MoveBase
{
    protected override void Perform()
    {
        if (target.AlreadyMoved)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/NoAction", GetComponent<Transform>().position);
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(InputManager.MousePos);

        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1 << 8);

        //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/LaserShot", GetComponent<Transform>().position);

        target.DoAction(new Vector2(hit.point.x, hit.point.z));

        target.AlreadyMoved = true;
    }

    protected override void UpdatePointer(Vector2 pos)
    {

    }
}
