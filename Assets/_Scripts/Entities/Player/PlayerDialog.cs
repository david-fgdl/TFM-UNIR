/* SCRIPT PARA MOSTRAR DIALOGOS DEL JUGADOR (FEEDBACK) */

using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PlayerDialog : MonoBehaviour
{

    /* VARIABLES */

    // REFERENCIAS
    [SerializeField] private GameObject _dialogObject;
    [SerializeField] private TMP_Text _dialogText;
    
    // INSTANCIA
    public static PlayerDialog Instance;

    /* METODOS BASICOS */

    // METODO AWAKE (Inicializar instancia)
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    /* METODOS DEL DIALOGO */

    // METODO DE LLAMADA A LA RUTINA PARA MOSTRAR DIALOGO
    public void ShowDialog(string msg)
    {
        StartCoroutine(ShowDialogRoutine(msg));
    }


    // RUTINA PARA MOSTRAR DIALOGO (OBJETOS Y PUERTA)
    private IEnumerator ShowDialogRoutine(string msg)
    {

        _dialogObject.SetActive(true);

        Debug.Log($"{msg}");
        _dialogText.text = $"{msg}";
        
        yield return new WaitForSeconds(3);

        _dialogObject.SetActive(false);

    }

}