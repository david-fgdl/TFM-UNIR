/* SCRIPT PARA TENER UN MAYOR CONTROL SOBRE EL AUDIO DEL JUEGO */

using UnityEngine;
using System;

[Serializable]
public class Sound : MonoBehaviour  // ! Sin uso de momento. Previsto por la escalabilidad
{

    /* VARIABLES */

    // VALORES DEL SONIDO
    public string SoundName;
    public AudioClip Clip;

    [Range(0f, 100f)]
    public float Volume;
    [Range(.1f, 3f)]
    public float Pitch;
}
