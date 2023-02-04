using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class InputManager : MonoBehaviour
{
    public static event Action<Vector2> PointerPositionChanged;
    public static event Action PrimaryAction;
    public static event Action SecondaryAction;

    public static event Action SelectNextPrimary;
    public static event Action SelectPreviousPrimary;

    public static event Action SelectNextSecondary;
    public static event Action SelectPreviousSecondary;

    public static event Action EndTurn;

    public static Vector2 MousePos { get; private set; }

    public static InputManager instance;

    private Input input;

    private void Awake()
    {
        Assert.IsNull(instance);
        instance = this;

        input = new Input();

        input.Mouse.Position.performed += (ctx) => 
        {
            MousePos = ctx.ReadValue<Vector2>();
            PointerPositionChanged?.Invoke(MousePos);
        };
        input.Mouse.Click.started += (ctx) => PrimaryAction?.Invoke();
        input.Mouse.RClick.started += (ctx) => SecondaryAction?.Invoke();

        input.Selection.NextPrimary.started += (ctx) => SelectNextPrimary?.Invoke();
        input.Selection.PreviousPrimary.started += (ctx) => SelectPreviousPrimary?.Invoke();

        input.Selection.NextSecondary.started += (ctx) => SelectNextSecondary?.Invoke();
        input.Selection.PreviousSecondary.started += (ctx) => SelectPreviousSecondary?.Invoke();

        input.Turn.EndTurn.started += (ctx) => EndTurn?.Invoke();
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
