using UnityEngine;

public class MoveToFunction : MoveBase
{
    [SerializeField]
    protected FunctionController controller;

    private Vector2? lastPos;

    public override void SetTarget(Unit target)
    {
        base.SetTarget(target);

        controller.transform.position = target.transform.position;

        controller.bounds = target.GetMovementBounds();

        controller.Generate();

        UpdatePointer(InputManager.MousePos);
    }

    public void UpdatePointer()
    {
        UpdatePointer(InputManager.MousePos);
    }

    protected override void UpdatePointer(Vector2 pos)
    {
        if (target != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);

            Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity);

            lastPos = controller.UpdateControl((hit.point - target.transform.position).XZToXY());
        }
    }
    
    protected override void Perform()
    {
        var v = lastPos.GetValueOrDefault();
        if (lastPos.HasValue && controller.bounds.Contains(v))
        {
            target.Move(new Vector3(v.x, 0f, v.y));
            controller.transform.position = target.transform.position;

            UpdatePointer();
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/OutOfBounds", GetComponent<Transform>().position);
        }
    }
}
