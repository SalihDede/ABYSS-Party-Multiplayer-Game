using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game4Manager : MonoBehaviour

{
    public static Game4Manager instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChange;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
   
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState) {
            case GameState.First:
                break;
            case GameState.Second:
                break;
            case GameState.Third:
                break;
            case GameState.Lose:
                break;
            case GameState.Victory:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState),newState,null);
        }
        OnGameStateChange?.Invoke(newState);
    }

    public enum GameState{
        First,
        Second,
        Third,
        Victory,
        Lose
    }
    
}
