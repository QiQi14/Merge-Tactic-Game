using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState gameState;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        instance = this; 
    }
    private void Start()
    {
        UpdateGameState(GameState.Menu);
        AudioManager.instance.PlayBGM("BGM_03");
    }

    public void OpenGameLevel()
    {
        UpdateGameState(GameState.GameLevel);
    }
    public void OpenMenuLevel()
    {
        UpdateGameState(GameState.Menu);
    }

    public void OpenVolumnSetting()
    {
        UpdateGameState(GameState.Volumn);
    }

    public void UpdateGameState(GameState newState)
    {
        gameState = newState;
        OnGameStateChanged?.Invoke(newState);
    }
}



public enum GameState
{
    GenerateMap,
    SpawnUnit,
    Menu,
    GameLevel,
    Volumn
}
