using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        ChangeState(GameState.Menu);
        // ChangeState(GameState.Game);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(GameState newState) {
        State = newState;

        switch (newState)
        {
            case GameState.Menu:
                HandleMenu();
                break;
            case GameState.Game:
                HandleGame();
                break;
            case GameState.Inventory:
                HandleInventory();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
            case GameState.EndGame:
                HandleEndGame();
                break;
            case GameState.Settings:
                HandleSettings();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState); //El ? chequea si es null
    }

    private void HandleMenu() {

    }

    private void HandleGame() {
        
    }

    private void HandleInventory() {
        // OPEN INVENTORY MENU
    }

    private void HandleGameOver() {
        
    }

    private void HandleEndGame() {
        
    }

    private void HandleSettings() {

    }


}

public enum GameState { // Podemos añadir estados dependiendo de los hitos conseguidos en la historia
    Menu,
    Game,
    Inventory,
    GameOver,
    EndGame,
    Settings
}
