using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToFunction : MonoBehaviour
{
    [SerializeField]
    private FunctionController controller;

    private Vector2 lastPos;

    private void Start()
    {
        InputManager.PointerPositionChanged += UpdatePointer;
        InputManager.PrimaryAction += Move;
    }

    private void UpdatePointer(Vector2 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);

        Physics.Raycast(ray,out RaycastHit hit, Mathf.Infinity);

        lastPos = controller.UpdateControl((hit.point - transform.position).XZToXY());
    }

    private void Move()
    {
        transform.position += new Vector3(lastPos.x, 0f, lastPos.y);
    }
}
