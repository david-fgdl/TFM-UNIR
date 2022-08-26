using System.Collections;
using System.IO;
using System;
using TMPro;
using UnityEngine;

public class FileUtils
{
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
}