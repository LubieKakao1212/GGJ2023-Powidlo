using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToFunction : MonoBehaviour
{
    [SerializeField]
    private FunctionController controller;

    private Vector3 lastPos;

    private void Start()
    {
        InputManager.PointerPositionChanged += UpdatePointer;
    }

    private void UpdatePointer(Vector2 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);

        Physics.Raycast(ray,out RaycastHit hit, Mathf.Infinity);

        lastPos = controller.UpdateControl(hit.point.XZToXY());
    }
}
