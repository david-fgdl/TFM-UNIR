/* SCRIPT DE UTILITES */
/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using System.Collections;
using System.IO;
using System;
using TMPro;
using UnityEngine;

public class Utils : MonoBehaviour
{

/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    /* METODOS PARA EL MANEJO DE ARRAYS */

    // METODO PARA SABER SI UN ENTERO ESTA CONTENIDO EN UN VECTOR DE ENTEROS
    public bool ContainsInt(int [] vector, int value, int previousIndex)
    {
        for (int i = 0; i <= previousIndex; i++) if (vector[i] == value) return true;  // Si el entero estA en el vector se devuelve true
        return false;  // Si no estA se devuelve false
    }


/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

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

/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    /* METODOS PARA EL MANEJO FICHEROS */

    // METODO PARA LEER EL CONTENIDO DE FICHEROS
    public string ReadFile (string filePath)
    {

        string fileInfo = null;  // String que almacenarA la informaciOn del archivo (Por defecto null)

        StreamReader fileStreamReader = new StreamReader(filePath);  // CreaciOn del StreamReader correspondiente al archivo cuyo nombre se pasa por parAmetro

        while (!fileStreamReader.EndOfStream) fileInfo = fileStreamReader.ReadLine();  // Lectura del archivo de inicio a fin

        fileStreamReader.Close();  // Cierre del archivo

        return fileInfo;  // DevoluciOn del contenido del archivo en forma de string

    }

    // METODO PARA LEER Y ALMACENAR UN FICHERO LINEA A LINEA
    public string [] ReadFileLineByLine (string filePath)
    {

        string[] fileLines = null;  // String que almacenarA la informaciOn del archivo (Por defecto " | ")

        fileLines = File.ReadAllLines(filePath);  // Lectura del arcihvo de inicio a fin

        return fileLines;  // DevoluciOn del contenido del archivo en forma de string

    }

    // METODO PARA LEER LOS DIALOGOS DE UN FICHERO EXTERNO
    public string ReadDialogs (int dialog_number)
    {

        string[] all_file_lines = ReadFileLineByLine (Application.dataPath + "/StreamingAssets/Dialogs/npc_dialogs.txt");  // String que almacena todas las lIneas del fichero

        string[] current_line = all_file_lines[dialog_number].Split('-');  // Se separa cada lInea en las dos partes (ID - Numero + NPC - y Mensaje)

        return current_line[1];  // Se devuelve el mensaje correspondiente

    }

    // METODO PARA MOSTRAR POR PANTALLA EL CONTENIDO DE UN FICHERO
    public IEnumerator ShowFileContent(TextMeshProUGUI textOutput, string filePath, float fadeInTime, float onScreenDuration, float fadeOutTime)
    {

        string[] text = ReadFileLineByLine(filePath);  // Vector que almacena el texto lInea a lInea

        // SE MUESTRA EL TEXTO LINEA A LINEA
        foreach (string line in text)
        {

            GameObject.Find("SceneController").GetComponent<AudioSource>().Play();

            StartCoroutine(ShowTextOnScreen(textOutput, line, fadeInTime, onScreenDuration, fadeOutTime));

            yield return new WaitForSeconds(fadeInTime + onScreenDuration + fadeOutTime + 1);  // Tiempo de espera entre lIneas

        }

    }

    // METODO PARA HACER SONAR EL AUDIO POR CADA LINEA
    public IEnumerator PlayAudioWhenShowFileContent(AudioSource audioOutput, AudioClip [] allAudios, string filePath, float fadeInTime, float onScreenDuration, float fadeOutTime)
    {

        string[] text = ReadFileLineByLine(filePath);  // Vector que almacena el texto lInea a lInea
        int vectorIterator = 0;

        // SE HACE SONAR CADA CLIP DE AUDIO
        foreach (string line in text)
        {

            audioOutput.clip = allAudios[vectorIterator];
            audioOutput.Play();
            vectorIterator++;

            yield return new WaitForSeconds(fadeInTime + onScreenDuration + fadeOutTime + 1);  // Tiempo de espera entre lIneas

        }

    }

/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /* METODOS PARA EL MANEJO DEL AUDIO */

        // METODO PARA CREAR UN LOOP DE MUSICA ADECUADO
        public IEnumerator StartMusicLoop (AudioSource musicClip1, AudioSource musicClip2, float minusTimeBeforeLoop)
    {

        musicClip1.Play();  // Suena el clip original

        yield return new WaitForSeconds(musicClip1.clip.length - minusTimeBeforeLoop);  // Se espera el tiempo adecuado para el loop (variarA en funciOn de los clips)

        musicClip2.Play();  // Suena el clip repetido

        yield return new WaitForSeconds(musicClip2.clip.length - minusTimeBeforeLoop);    // Se espera el tiempo adecuado para el loop (variarA en funciOn de los clips)

        StartCoroutine(StartMusicLoop(musicClip1, musicClip2, minusTimeBeforeLoop));  // Se repite la subrutina

    }


/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

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

/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    /* METODOS DE DEBUG */

    // METODO PARA OBTENER UN DEBUG CON MAS INFORMACION (CLASE, METODO, ETC...)
    public static void BetterLog(string className, string methodName, string msg)
    {
        string time = DateTime.Now.ToString("h:mm:ss tt");
        Debug.Log($"{time} - Class [{className}], on method {methodName}: {msg}");
    }

/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

}

/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
