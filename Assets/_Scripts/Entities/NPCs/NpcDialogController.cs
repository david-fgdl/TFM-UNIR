/* SCRIPT PARA CONTROLAR EL COMPORTAMIENTO DEL DIALOGO DE LOS NPCS */
/*-----------------------------------------------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogController : MonoBehaviour
{

    /* VARIABLES */

    private FileUtils _fileUtils = new FileUtils();

    // NUMERO DEL DIALOGO ACTUAL
    private int _currentDialogNumber = 0;
    
    // REFERENCIAS
    [SerializeField] private GameObject _dialogBoxGameobject;  // Referencia al GameObject de diAlogo de la UI
    // [SerializeField] private Utils _utils;  // Referencia al script de utils de la escena

    /* METODOS BASICOS */

    // METODO START
    // El mEtodo Start es llamada antes de actualizar el primer frame
    void Start()
    {

        _dialogBoxGameobject.SetActive(false);  // Desactivar cuadro de diAlogo
    }

    /* METODOS DE TRIGGER*/

    // ACCION DE ENTRADA EN EL TRIGGER
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))  // Si el jugador entra al Trigger
        {
            _dialogBoxGameobject.GetComponentInChildren<TextMeshProUGUI>().text = _fileUtils.ReadDialogs(_currentDialogNumber);  // Establecer el diAlogo actual del NPC en el cuadro de diAlogo
            _dialogBoxGameobject.SetActive(true);  // Establecer el cuadro de diAlogo
            GetComponents<AudioSource>()[0].Play();  // Hacer sonar el sonido de diAlogo abierto
        }

    }

    // ACCION DE SALIDA DEL TRIGGER
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // Si el jugador sale del Trigger
        {
            _dialogBoxGameobject.SetActive(false);  // Deshabilitar el cuadro de diAlogo
            GetComponents<AudioSource>()[1].Play();  // Hacer sonar el sonido de diAlogo cerrado
        }
    }

    /* GETTERS Y SETTERS */

    // NUMERO DEL DIALOGO ACTUAL
    public int getCurrentDialogNumber(){ return _currentDialogNumber; }
    public void setCurrentDialogNumber(int newCurrentDialogNumber){ _currentDialogNumber = newCurrentDialogNumber; }

}

