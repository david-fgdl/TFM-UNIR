/* SCRIPT QUE CONTROLA LA ESCENA PRINCIPAL */

using System.Collections;
using UnityEngine;

public class RoomTestController : MonoBehaviour
{

    /* METODOS BASICOS */

    // METODO START
    // Start es un mEtodo llamado antes del primer frame
    private void Start()
    {

        // ESTABLECER DIALOGOS INICIALES
        GameObject.Find("Son").transform.GetChild(0).GetChild(0).GetComponent<NPCDialogController>().SetCurrentDialogNumber(1);  // Establecer el diAlogo inicial del hijo
        GameObject.Find("Daughter").transform.GetChild(0).GetChild(0).GetComponent<NPCDialogController>().SetCurrentDialogNumber(2);  // Establecer el diAlogo inicial de la hija
        GameObject.Find("Father").transform.GetChild(0).GetChild(0).GetComponent<NPCDialogController>().SetCurrentDialogNumber(3);  // Establecer el diAlogo inicial del padre
        GameObject.Find("Mother").transform.GetChild(0).GetChild(0).GetComponent<NPCDialogController>().SetCurrentDialogNumber(4);  // Establecer el diAlogo inicial de la madre
        
    }

}
