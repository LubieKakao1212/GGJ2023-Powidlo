using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

    public static event Action<int> SelectNumber;
    
    public static event Action EndTurn;

    public static Vector2 MousePos { get; private set; }

    public static InputManager instance;

    private Input input;

    private bool _suppressNext;

    private Queue<Action> _delayQueue = new();
    
    public void SuppressNextSecondary() {
        _suppressNext = true;
    }
    
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
        //dirty hack : ( [for non-existent issue :(( ]
        input.Mouse.RClick.started += (ctx) => 
            StartCoroutine(DelayByFrames(0, () => {
                if (!this._suppressNext) {
                    SecondaryAction?.Invoke();
                }
                this._suppressNext = false;
            }));
            // _delayQueue.Enqueue(() => );
        input.Selection.NextPrimary.started += (ctx) => SelectNextPrimary?.Invoke();
        input.Selection.PreviousPrimary.started += (ctx) => SelectPreviousPrimary?.Invoke();

        input.Selection.NextSecondary.started += (ctx) => SelectNextSecondary?.Invoke();
        input.Selection.PreviousSecondary.started += (ctx) => SelectPreviousSecondary?.Invoke();
        input.Selection.Number.started += ctx => {
            var ctrlName = ctx.control.name;
            if (int.TryParse(ctrlName.Substring(ctrlName.Length - 1), out var value)) {
                SelectNumber?.Invoke(value);
            }
            else {
                throw new Exception("wrong key");
            }
        };


        input.Turn.EndTurn.started += (ctx) => EndTurn?.Invoke();
        
        
    }

    private IEnumerator DelayByFrames(int frames, Action action) {
        for (int i = 0; i < frames; i++) {
            yield return null;
        }
        action();
    }
    
    private void Update() {
        foreach (var action in _delayQueue) {
            action();
        }
        _delayQueue.Clear();
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
