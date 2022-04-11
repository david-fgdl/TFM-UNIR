using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel, 
                                        settingsPanel, 
                                        gameOverPanel, 
                                        endGamePanel, 
                                        inventoryPanel, 
                                        gameUIPanel;
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state) {
        
        // MENU PANELS
        menuPanel.SetActive(state == GameState.Menu);
        settingsPanel.SetActive(state == GameState.Settings);

        // GAME GENERAL STATE PANELS
        gameOverPanel.SetActive(state == GameState.GameOver);
        endGamePanel.SetActive(state == GameState.EndGame);

        // IN-GAME PANELS
        inventoryPanel.SetActive(state == GameState.Inventory);
        gameUIPanel.SetActive(state == GameState.Game);
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;    
    }
}
