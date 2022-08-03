using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel, 
                                        _settingsPanel, 
                                        _gameOverPanel, 
                                        _endGamePanel, 
                                        _inventoryPanel, 
                                        _gameUIPanel,
                                        _loadingPanel;
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state) 
    {
        // MENU PANELS
        _menuPanel.SetActive(state == GameState.Menu);
        _settingsPanel.SetActive(state == GameState.Settings);

        // GAME GENERAL STATE PANELS
        _gameOverPanel.SetActive(state == GameState.GameOver);
        _endGamePanel.SetActive(state == GameState.EndGame);
        _loadingPanel.SetActive(state == GameState.Loading);

        // IN-GAME PANELS
        _inventoryPanel.SetActive(state == GameState.Inventory);
        _gameUIPanel.SetActive(state == GameState.Game);
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;    
    }
}
