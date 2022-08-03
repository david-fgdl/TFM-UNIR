using UnityEngine;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    public void Play() 
    {
        Debug.Log("LET'S GAME");
        GameManager.Instance.ChangeState(GameState.Game);
    }

    public void ExitGame()
    {
        Debug.Log("QUITING GAME");
        Application.Quit();
        #if (UNITY_EDITOR) 
            EditorApplication.isPlaying = false;
        #endif
    }
}
