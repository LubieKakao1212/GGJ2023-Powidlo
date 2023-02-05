using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveToFunction : MonoBehaviour
{
    [SerializeField]
    private Unit target;

    [SerializeField]
    private FunctionController controller;

    private Vector2? lastPos;

    [SerializeField]
    private bool startDisabled;
    private bool isEnabled;

    public void SetTargetController(Unit target, FunctionController controller)
    {
        this.target = target;
        SetController(controller);
    }

    public void SetController(FunctionController controller)
    {
        this.controller = controller;
        controller.transform.position = target.transform.position;

        controller.Generate();

        UpdatePointer(InputManager.MousePos);
    }

    public void DisabeControl()
    {
        InputManager.PointerPositionChanged -= UpdatePointer;
        InputManager.PrimaryAction -= Move;
        isEnabled = false;
    }

    public void EnableControl()
    {
        if (!isEnabled)
        {
            InputManager.PointerPositionChanged += UpdatePointer;
            InputManager.PrimaryAction += Move;
            isEnabled = true;
        }
    }

    public void UpdatePointer()
    {
        UpdatePointer(InputManager.MousePos);
    }

    private void OnEnable()
    {
        EnableControl();
    }

    private void Start()
    {
        if (startDisabled)
        {
            DisabeControl();
        }
    }

    private void OnDisable()
    {
        DisabeControl();
    }

    private void UpdatePointer(Vector2 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);

        Physics.Raycast(ray,out RaycastHit hit, Mathf.Infinity);

        lastPos = controller.UpdateControl((hit.point - target.transform.position).XZToXY());
    }
    
    private void Move()
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
