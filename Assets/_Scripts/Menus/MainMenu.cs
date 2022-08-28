/* SCRIPT QUE CONTROLA EL FUNCIONAMIENTO DEL MENU PRINCIPAL */

using UnityEngine;
using UnityEditor;

public class MainMenu : MonoBehaviour
{

    /* METODOS DEL MENU */

    // METODO PARA INICIAR EL JUEGO
    public void Play() 
    {
        Debug.Log("LET'S GAME");
        GameManager.Instance.ChangeState(GameState.Game);
    }

    // METODO PARA SALIR DEL JUEGO
    public void ExitGame()
    {
        Debug.Log("QUITING GAME");
        Application.Quit();
        #if (UNITY_EDITOR) 
            EditorApplication.isPlaying = false;
        #endif
    }
}
