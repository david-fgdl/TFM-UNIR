/* SCRIPT QUE CONTROLA EL FUNCIONAMIENTO DEL MENU PRINCIPAL */

using UnityEngine;
using UnityEditor;

public class MainMenu : MonoBehaviour
{

    /* METODOS DEL MENU */

    // METODO PARA INICIAR EL JUEGO
    public void Play() 
    {
        GameManager.Instance.ChangeState(GameState.Game);
    }

    // METODO PARA SALIR DEL JUEGO
    public void ExitGame()
    {
        Application.Quit();
        #if (UNITY_EDITOR) 
            EditorApplication.isPlaying = false;
        #endif
    }
}
