/* SCRIPT DE UTILIDADES BASICAS */

using System.Collections;
using System.IO;
using System;
using TMPro;
using UnityEngine;

public class Utils
{

    /* METODOS PARA EL MANEJO DE ARRAYS */

    // METODO PARA SABER SI UN ENTERO ESTA CONTENIDO EN UN VECTOR DE ENTEROS
    public bool ContainsInt(int [] vector, int value, int previousIndex)
    {
        for (int i = 0; i <= previousIndex; i++) if (vector[i] == value) return true;  // Si el entero estA en el vector se devuelve true
        return false;  // Si no estA se devuelve false
    }

    /* METODOS PARA DEPURAR DE FORMA CONTROLADA */

    // METODO AUXILIAR PARA CONTROLAR UNA PARADA EN LA EJECUCION DEL PROGRAMA SI UN GAMEOBJECT NO EXISTE
    public GameObject SearchGameObject(string gameObjectNameInEditor)
    {

        // SI EL GAMEOBJECT EXISTE SE DEVUELVE, SI NO, SE INDICA POR PANTALLA QUE NO EXISTE. DE ESTE MODO SE RECONOCE MAS FACILMENTE EL ERROR QUE PUEDE BLOQUEAR LA EJECUCION DEL RESTO DEL CODIGO
        if (GameObject.Find(gameObjectNameInEditor) != null) return GameObject.Find(gameObjectNameInEditor);
        else
        {
            Debug.Log("ERROR EN LA BUSQUEDA: El gameobject de nombre \"" + gameObjectNameInEditor + "\" no existe...");
            return null;
        }

    }

    /* METODOS DE DEBUG */

    // METODO PARA OBTENER UN DEBUG CON MAS INFORMACION (CLASE, METODO, ETC...)
    public static void BetterLog(string className, string methodName, string msg)
    {
        string time = DateTime.Now.ToString("h:mm:ss tt");
        Debug.Log($"{time} - Class [{className}], on method {methodName}: {msg}");
    }

}

