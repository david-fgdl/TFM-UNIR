/* SCRIPT DE UTILITES */
/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Utils : MonoBehaviour
{

/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    /* METODOS PARA EL MANEJO DE ARRAYS */

    // METODO PARA SABER SI UN ENTERO ESTA CONTENIDO EN UN VECTOR DE ENTEROS
    public bool ContainsInt(int [] vector, int value, int previous_index)
    {
        for (int i = 0; i <= previous_index; i++) if (vector[i] == value) return true;  // Si el entero estA en el vector se devuelve true
        return false;  // Si no estA se devuelve false
    }


/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    /* METODOS PARA EL MANEJO DE TEXTO (TEXT MESH PRO) */

    // SUBRUTINA PARA MOSTRAR EL TEXTO POR PANTALLA
    public IEnumerator ShowTextOnScreen(TextMeshProUGUI text_object, string new_text, float fading_in_time, float text_on_screen_duration, float fading_out_time)
    {

        text_object.color = new Color(text_object.color.r, text_object.color.g, text_object.color.b, 0);  // Se pone el alpha del texto a 0
        text_object.text = new_text;  // Se pasa el nuevo texto

        // FADE IN DEL TEXTO
        StartCoroutine(TextFadeIn(text_object, fading_in_time));
        yield return new WaitForSeconds(fading_in_time);

        // DURACION DEL TEXTO EN PANTALLA
        yield return new WaitForSeconds(text_on_screen_duration);

        // FADE OUT DEL TEXTO
        StartCoroutine(TextFadeOut(text_object, fading_out_time));

    }

    // SUBRUTINA PARA HACER EL FADE IN DEL TEXTO BASADO EN: https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/
    public IEnumerator TextFadeIn(TextMeshProUGUI text_object, float fading_time)
    {

        text_object.color = new Color(text_object.color.r, text_object.color.g, text_object.color.b, 0);  // Se pone el alpha del texto a 0

        // SE HACE EL FADE IN CON BASE EN EL PARAMETRO RECIBIDO
        while (text_object.color.a < 1.0f)
        {
            text_object.color = new Color(text_object.color.r, text_object.color.g, text_object.color.b, text_object.color.a + (Time.deltaTime / fading_time));
            yield return null;
        }

    }

    // SUBRUTINA PARA HACER EL FADE OUT DEL TEXTO BASADO EN: https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/
    public IEnumerator TextFadeOut(TextMeshProUGUI text_object, float fading_time)
    {

        text_object.color = new Color(text_object.color.r, text_object.color.g, text_object.color.b, 1);  // Se pone el alpha del texto a 1

        // SE HACE EL FADE OUT CON BASE EN EL PARAMETRO RECIBIDO
        while (text_object.color.a > 0.0f)
        {
            text_object.color = new Color(text_object.color.r, text_object.color.g, text_object.color.b, text_object.color.a - (Time.deltaTime / fading_time));
            yield return null;
        }

    }

/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    /* METODOS PARA EL MANEJO FICHEROS */

    // METODO PARA LEER EL CONTENIDO DE FICHEROS
    public string ReadFile(string file_path)
    {

        string file_info = null;  // String que almacenar� la informaci�n del archivo (Por defecto null)

        StreamReader file_stream_reader = new StreamReader(file_path);  // Creaci�n del StreamReader correspondiente al archivo cuyo nombre se pasa por par�metro

        while (!file_stream_reader.EndOfStream) file_info = file_stream_reader.ReadLine();  // Lectura del archivo de inicio a fin

        file_stream_reader.Close();  // Cierre del archivo

        return file_info;  // Devoluci�n del contenido del archivo en forma de string

    }

    // METODO PARA LEER Y ALMACENAR UN FICHERO LINEA A LINEA
    public string [] ReadFileLineByLine(string file_path)
    {

        string[] file_lines = null;  // String que almacenar� la informaci�n del archivo (Por defecto " | ")

        file_lines = File.ReadAllLines(file_path);  // Lectura del arcihvo de inicio a fin

        return file_lines;  // Devoluci�n del contenido del archivo en forma de string

    }

    // METODO PARA MOSTRAR POR PANTALLA EL CONTENIDO DE UN FICHERO
    public IEnumerator ShowFileContent(TextMeshProUGUI text_output, string file_path, float fading_in_time, float text_on_screen_duration, float fading_out_time)
    {

        string[] text = ReadFileLineByLine(file_path);  // Vector que almacena el texto lInea a lInea

        // SE MUESTRA EL TEXTO LINEA A LINEA
        foreach (string line in text)
        {

            GameObject.Find("SceneController").GetComponent<AudioSource>().Play();

            StartCoroutine(ShowTextOnScreen(text_output, line, fading_in_time, text_on_screen_duration, fading_out_time));

            yield return new WaitForSeconds(fading_in_time + text_on_screen_duration + fading_out_time + 1);  // Tiempo de espera entre lIneas

        }

    }

    // METODO PARA HACER SONAR EL AUDIO POR CADA LINEA
    public IEnumerator PlayAudioWhenShowFileContent(AudioSource audio_output, AudioClip [] all_audios, string file_path, float fading_in_time, float text_on_screen_duration, float fading_out_time)
    {

        string[] text = ReadFileLineByLine(file_path);  // Vector que almacena el texto lInea a lInea
        int vector_iterator = 0;

        // SE HACE SONAR CADA CLIP DE AUDIO
        foreach (string line in text)
        {

            audio_output.clip = all_audios[vector_iterator];
            audio_output.Play();
            vector_iterator ++;

            yield return new WaitForSeconds(fading_in_time + text_on_screen_duration + fading_out_time + 1);  // Tiempo de espera entre lIneas

        }

    }

/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /* METODOS PARA EL MANEJO DEL AUDIO */

        // METODO PARA CREAR UN LOOP DE MUSICA ADECUADO
        public IEnumerator StartMusicLoop (AudioSource music_clip_1, AudioSource music_clip_2, float minus_time_before_loop)
    {

        music_clip_1.Play();  // Suena el clip original

        yield return new WaitForSeconds(music_clip_1.clip.length - minus_time_before_loop);  // Se espera el tiempo adecuado para el loop (variarA en funciOn de los clips)

        music_clip_2.Play();  // Suena el clip repetido

        yield return new WaitForSeconds(music_clip_2.clip.length - minus_time_before_loop);    // Se espera el tiempo adecuado para el loop (variarA en funciOn de los clips)

        StartCoroutine(StartMusicLoop(music_clip_1, music_clip_2, minus_time_before_loop));  // Se repite la subrutina

    }


/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    /* METODOS PARA DEPURAR DE FORMA CONTROLADA */

    // METODO AUXILIAR PARA CONTROLAR UNA PARADA EN LA EJECUCION DEL PROGRAMA SI UN GAMEOBJECT NO EXISTE
    public GameObject SearchGameObject(string gameobject_name_in_editor)
    {

        // SI EL GAMEOBJECT EXISTE SE DEVUELVE, SI NO, SE INDICA POR PANTALLA QUE NO EXISTE. DE ESTE MODO SE RECONOCE MAS FACILMENTE EL ERROR QUE PUEDE BLOQUEAR LA EJECUCION DEL RESTO DEL CODIGO
        if (GameObject.Find(gameobject_name_in_editor) != null) return GameObject.Find(gameobject_name_in_editor);
        else
        {
            Debug.Log("ERROR EN LA BUSQUEDA: El gameobject de nombre \"" + gameobject_name_in_editor + "\" no existe...");
            return null;
        }

    }

/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    /* METODOS DE DEBUG */

    // METODO PARA OBTENER UN DEBUG CON MÁS INFORMACIÓN (CLASE, MÉTODO, ETC...)
    public static void BetterLog(string className, string methodName, string msg)
    {
        string time = DateTime.Now.ToString("h:mm:ss tt");
        Debug.Log($"{time} - Class [{className}], on method {methodName}: {msg}");
    }

/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

}

/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
