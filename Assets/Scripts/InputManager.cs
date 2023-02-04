using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class InputManager : MonoBehaviour
{
    public static event Action<Vector2> PointerPositionChanged;
    public static event Action PrimaryAction;

    public static InputManager instance;

    private Input input;

    private void Awake()
    {
        Assert.IsNull(instance);
        instance = this;

        input = new Input();

        input.Mouse.Position.performed += (ctx) => PointerPositionChanged?.Invoke(ctx.ReadValue<Vector2>());
        input.Mouse.Click.performed += (ctx) => PrimaryAction?.Invoke();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
