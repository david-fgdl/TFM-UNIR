using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameObject _loadingScreen;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        ChangeState(GameState.Menu);
    }

    // Update is called once per frame
    private void Update()
    {
        
        // Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>().currentActionMap);
    }

    public void ChangeState(GameState newState) 
    {
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
            case GameState.Loading:
                HandleLoading();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        PauseOrResume();

        OnGameStateChanged?.Invoke(newState); //El ? chequea si es null
    }

    private void PauseOrResume()
    {
        if (State != GameState.Game)
        { // We're on a menu
            Time.timeScale = 0f;
            _playerInput.SwitchCurrentActionMap("UI");
            Cursor.lockState = CursorLockMode.None;
        }
        else
        { // We're in game
            Time.timeScale = 1f;
            _playerInput.SwitchCurrentActionMap("Player");
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void HandleMenu() {
        // Menu principal

        // inicializamos rapido el inventario
    }

    private void HandleGame() {
        // OPCIONES DE OPTIMIZACION
        Application.targetFrameRate = 60;
        
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

    private void HandleLoading() {

    }


}

public enum GameState { // Podemos añadir estados dependiendo de los hitos conseguidos en la historia
    Menu,
    Game,
    Inventory,
    GameOver,
    EndGame,
    Settings,
    Loading
}
