
/* SCRIPT PARA CONTROLAR EL COMPORTAMIENTO DEL ENEMIGO */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    /* VARIABLES */

    // INSTANCIAS
    public static GameManager Instance;
    public GameState State;  // Estado del juego

    // EVENTOS
    public static event Action<GameState> OnGameStateChanged;

    // REFERENCIAS
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameObject _loadingScreen;

    /* METODOS BASICOS*/

    // METODO AWAKE
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    // METODO START
    private void Start()
    {
        ChangeState(GameState.Menu);
    }

    // Update is called once per frame
    private void Update()
    {
        
        // Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>().currentActionMap);
    }

    /* METODOS PROPIPOS DEL GAME MANAGER */

    // METODO PARA CAMBIAR EL ESTADO DEL JUEGO
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

    // METODO PARA GESTIONAR LAS PAUSAS DEL JUEGO
    private void PauseOrResume()
    {

        // ESTAMOS EN UN MENU
        if (State != GameState.Game)
        {
            Time.timeScale = 0f;
            _playerInput.SwitchCurrentActionMap("UI");
            Cursor.lockState = CursorLockMode.None;
        }

        // ESTAMOS EN EL JUEGO
        else
        {
            Time.timeScale = 1f;
            _playerInput.SwitchCurrentActionMap("Player");
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    /* MANEJADORAS */

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

/* LISTA DE ESTADOS DEL JUEGO */
public enum GameState { // Podemos añadir estados dependiendo de los hitos conseguidos en la historia
    Menu,
    Game,
    Inventory,
    GameOver,
    EndGame,
    Settings,
    Loading
}
