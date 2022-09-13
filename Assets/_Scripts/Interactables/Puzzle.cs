/* SCRIPT QUE CONTIENE LA CLASE ABSTRACTA "PUZZLE" */
/* Implementable en cualquier script de puzzle que podamos tener. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{

    /* VARIABLES */

    // FLAGS
    protected bool _isCompleted = false; // Marcamos si está completado.

    // TIPO DE PUZLE
    public enum Type // Definimos el tipo de puzle.
    { 
        None, // None: No existe puzle.
        Object, // Requiere de un objeto o varios.
        OneWay // Solo se puede abrir desde un lado.
    }
    
}
