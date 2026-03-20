using FMODUnity;
using UnityEngine;

public class UnitActionAttack : UnitActionBase
{
    protected override void Perform()
    {
        if (unit.AlreadyMoved)
        {
            RuntimeManager.PlayOneShot("event:/UI/NoAction", GetComponent<Transform>().position);
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(InputManager.MousePos);

        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1 << 8);

        //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/LaserShot", GetComponent<Transform>().position);

        unit.DoAction(new Vector2(hit.point.x, hit.point.z));

        unit.AlreadyMoved = true;
    }

    protected override void UpdatePointer(Vector2 pos)
    {

    }
}
