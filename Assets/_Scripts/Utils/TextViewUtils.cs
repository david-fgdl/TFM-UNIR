/* SCRIPT DE UTILIDADES PARA LA VISUALIZACION DE TEXTO */

using System.Collections;
// using System.IO;
using System;
using TMPro;
using UnityEngine;
using System.IO;
using System.ComponentModel;
using UnityEngine.Scripting;

public class TextViewUtils : MonoBehaviour
{

    /* VARIABLES */

    private FileUtils _fileUtils;  // Referencia a FileUtils

    //public TextViewUtils Instance;  // Instancia

    /* METODOS BASICOS */

    // METODO AWAKE
    private void Awake()
    {

        // ASIGANACION DE INSTANCIA
        /*if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;*/

        // RECOGIDA DE REFERENCIAS
        _fileUtils = GetComponent<FileUtils>();

    }


    /* METODOS PARA EL MANEJO DE TEXTO (TEXT MESH PRO) */

    // SUBRUTINA PARA MOSTRAR EL TEXTO POR PANTALLA
    public IEnumerator ShowTextOnScreen(TextMeshProUGUI textObject, string newText, float fadeInTime, float onScreenDuration, float fadeOutTime)
    {

        textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, 0);  // Se pone el alpha del texto a 0
        textObject.text = newText;  // Se pasa el nuevo texto

        // FADE IN DEL TEXTO
        StartCoroutine(TextFadeIn(textObject, fadeInTime));
        yield return new WaitForSeconds(fadeInTime);

        // DURACION DEL TEXTO EN PANTALLA
        yield return new WaitForSeconds(onScreenDuration);

        // FADE OUT DEL TEXTO
        StartCoroutine(TextFadeOut(textObject, fadeOutTime));

    }

    // SUBRUTINA PARA HACER EL FADE IN DEL TEXTO BASADO EN: https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/
    public IEnumerator TextFadeIn(TextMeshProUGUI textObject, float fadingTime)
    {

        textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, 0);  // Se pone el alpha del texto a 0

        // SE HACE EL FADE IN CON BASE EN EL PARAMETRO RECIBIDO
        while (textObject.color.a < 1.0f)
        {
            textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, textObject.color.a + (Time.deltaTime / fadingTime));
            yield return null;
        }

    }

    // SUBRUTINA PARA HACER EL FADE OUT DEL TEXTO BASADO EN: https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/
    public IEnumerator TextFadeOut(TextMeshProUGUI textObject, float fadingTime)
    {

        textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, 1);  // Se pone el alpha del texto a 1

        // SE HACE EL FADE OUT CON BASE EN EL PARAMETRO RECIBIDO
        while (textObject.color.a > 0.0f)
        {
            textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, textObject.color.a - (Time.deltaTime / fadingTime));
            yield return null;
        }
    }

    // METODO PARA MOSTRAR POR PANTALLA EL CONTENIDO DE UN FICHERO
    public IEnumerator ShowFileContent(TextMeshProUGUI textOutput, string filePath, float fadeInTime, float onScreenDuration, float fadeOutTime)
    {

        string[] text = _fileUtils.ReadFileLineByLine(filePath);  // Vector que almacena el texto lInea a lInea

        // SE MUESTRA EL TEXTO LINEA A LINEA
        foreach (string line in text)
        {

            GameObject.Find("SceneController").GetComponent<AudioSource>().Play();

            StartCoroutine(ShowTextOnScreen(textOutput, line, fadeInTime, onScreenDuration, fadeOutTime));

            yield return new WaitForSeconds(fadeInTime + onScreenDuration + fadeOutTime + 1);  // Tiempo de espera entre lIneas

        }

    }
    

}