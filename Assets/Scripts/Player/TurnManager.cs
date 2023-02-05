using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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

    [SerializeField]
    private float maxTurnTime;
    [SerializeField]
    private float overTime;
    private bool isOverTime;

    [SerializeField]
    private TextMeshProUGUI clock;

    [SerializeField]
    private GameObject board;

    private float currentTime = 0;

    private int currentPlayerIndex;

    public void NextTurn()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/RoundSwap", GetComponent<Transform>().position);
        
        //moveManager.DisabeControl();
        ResetClock();

        currentPlayerIndex = (++currentPlayerIndex) % players.Count;

        LeanTween.rotateY(board, 180f * currentPlayerIndex, 1f);

        CurrentPlayer.DisabeControl();
        CurrentPlayer.SelectedUnitChanged -= OnPlayerUnitChanged;

        CurrentPlayer = players[currentPlayerIndex];
        
        CurrentPlayer.EnableControl();
        CurrentPlayer.SelectedUnitChanged += OnPlayerUnitChanged;
        moveManager.SetTarget(CurrentPlayer.CurrentUnit);
        
        TurnPasses?.Invoke();
    }

    public void RemovePlayer(Player player)
    {
        players.Remove(player);
        //Will not work with more players
        Debug.Log("You won!!");
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > maxTurnTime) 
        {
            NextTurn(); 
        }

        float timeleft = (maxTurnTime - currentTime);

        clock.text = timeleft.ToString("00");
        if (!isOverTime && timeleft < overTime)
        {
            isOverTime= true;
            clock.color = Color.red;

            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Countdown", 1);
        }
    }

    private void ResetClock()
    {
        currentTime = 0;
        clock.color = Color.white;

        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Countdown", 0);
        isOverTime = false;
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
