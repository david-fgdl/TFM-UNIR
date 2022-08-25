using System.Collections;
using UnityEngine;

public class RoomTestController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        // ESTABLECER DIALOGOS INICIALES
        GameObject.Find("Son").transform.GetChild(0).GetChild(0).GetComponent<NpcDialogController>().setCurrentDialogNumber(1);  // Establecer el diAlogo inicial del hijo
        GameObject.Find("Daughter").transform.GetChild(0).GetChild(0).GetComponent<NpcDialogController>().setCurrentDialogNumber(2);  // Establecer el diAlogo inicial de la hija
        GameObject.Find("Father").transform.GetChild(0).GetChild(0).GetComponent<NpcDialogController>().setCurrentDialogNumber(3);  // Establecer el diAlogo inicial del padre
        GameObject.Find("Mother").transform.GetChild(0).GetChild(0).GetComponent<NpcDialogController>().setCurrentDialogNumber(4);  // Establecer el diAlogo inicial de la madre
    }

    

}
