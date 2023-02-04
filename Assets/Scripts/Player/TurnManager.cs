using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TurnManager : MonoBehaviour
{
    public static event Action TurnPasses;

    public static TurnManager Instance { get; private set; }
    
    public Player CurrentPlayer { get; private set; }

    [SerializeField]
    private MovementManager moveManager;
    
    [SerializeField]
    private List<Player> players;

    private int currentPlayerIndex;

    public void NextTurn()
    {
        //moveManager.DisabeControl();
        currentPlayerIndex = (++currentPlayerIndex) % players.Count;
        
        CurrentPlayer.DisabeControl();
        CurrentPlayer.SelectedUnitChanged -= OnPlayerUnitChanged;

        CurrentPlayer = players[currentPlayerIndex];
        
        CurrentPlayer.EnableControl();
        CurrentPlayer.SelectedUnitChanged += OnPlayerUnitChanged;
        moveManager.SetTarget(CurrentPlayer.CurrentUnit);

        TurnPasses?.Invoke();
    }

    private void Start()
    {
        Assert.IsNull(Instance);
        Instance = this;

        CurrentPlayer = players[0];
        currentPlayerIndex = -1;
        moveManager.SetupCache();
        moveManager.NextFunction();
        moveManager.EnableControl();
        NextTurn();
    }

    private void OnEnable()
    {
        InputManager.EndTurn += NextTurn;
    }

    private void OnDisable()
    {
        InputManager.EndTurn -= NextTurn;
    }

    private void OnPlayerUnitChanged()
    {
        var unit = CurrentPlayer.CurrentUnit;
        if (unit.AlreadyMoved)
        {
            moveManager.DisabeControl();
            moveManager.gameObject.SetActive(false);
        }
        else
        {
            moveManager.EnableControl();
            moveManager.gameObject.SetActive(true);
            moveManager.SetTarget(CurrentPlayer.CurrentUnit);
        }
    } 
}
