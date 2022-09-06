/* SCRIPT QUE REGULA EL COMPORTAMIENTO DE LAS PUERTAS */

using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour {

    /* VARIABLES */

    // PARAMETROS DE LA PUERTA
    public string Id; // Id de la puerta
    public string DoorName; // Nombre de la puerta

    // REFERENCIAS
    public InventoryItemData UnlockObject; // Objeto que desbloquea la puerta
    public Puzzle.Type Type; // Tipo de puzzle
    private GameObject _enemyRef; // Referencia del enemigo

    // FLAGS

    private bool _onlyOnce; // Switch para evitar muchas ejecuciones
    

    #region Open Variables
    public bool IsOpen = false; // Estado de la puerta
    public bool IsInverted = false; // Marca dónde están ubicadas las bisagras
    public float ClosedRotation; // Rotacion de la puerta cerrada
    public float OpenedRotation; // Rotacion de la puerta abierta
    #endregion

    // VALORES DE TRANSFORMACION
    [SerializeField] private Transform _pivot; // Posición de la bisagra
    private float _currentRotation; // Rotación actual de la puerta

    // VARIABLES DE SONIDO
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _doorUnlockedSound; // Sonido de Sound Effect from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=music&amp;utm_content=95884">Pixabay</a>
    [SerializeField] private AudioClip _doorOpeningSound; // Sonido modificado de Sound Effect from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=music&amp;utm_content=43633">Pixabay</a>
    [SerializeField] private AudioClip _doorClosingSound; // Sonido modificado de Sound Effect from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=music&amp;utm_content=43633">Pixabay</a>

    /* METODOS BASICOS */

    // METODO START
    // Start es llamado antes del primer frame
    private void Start()
    {
        _currentRotation = ClosedRotation;
        _enemyRef = GameObject.FindGameObjectWithTag("Enemy");
        _onlyOnce = true;
    }

    // METODO UPDATE
    // Update es llamado una vez por frame
    private void Update()
    {
        // Llamada a la rutina para que el enemigo cierre la puerta
        if (IsOpen && Vector3.Distance(transform.position, _enemyRef.transform.position) < 3 && _onlyOnce)
        {
            _onlyOnce = false;
            StartCoroutine(OpenClose(IsOpen)); // Cierre de la puerta
        }
    }

    /* METODOS DE LA PUERTA */

    // METODO DE APERTURA / CIERRE DE LA PUERTA
    private IEnumerator OpenClose(bool isOpen) 
    {

        // GESTION DE SONIDOS DE LA PUERTA
        if (!IsOpen) _audioSource.PlayOneShot(_doorOpeningSound);
        else _audioSource.PlayOneShot(_doorClosingSound);

        // AJUSTES DE TRANSFORMACION
        if (!IsInverted) 
        { // Se comprueba si la puerta estA invertida
            if (!IsOpen) // Se comprueba si la puerta estA cerrada
            {
                for (float i = _currentRotation; i >= OpenedRotation; i--)
                {
                    _currentRotation = i;
                    _pivot.eulerAngles = new Vector3(0, _currentRotation, 0);
                    yield return new WaitForSeconds(0.01f);
                }
            }
            else
            {
                for (float i = _currentRotation; i < ClosedRotation; i++)
                {
                    _currentRotation = i;
                    _pivot.eulerAngles = new Vector3(0, _currentRotation, 0);
                    yield return new WaitForSeconds(0.01f);
                }
            }
        } else 
        {
            if (!IsOpen)
            {
                for (float i = _currentRotation; i <= OpenedRotation; i++)
                {
                    _currentRotation = i;
                    _pivot.eulerAngles = new Vector3(0, _currentRotation, 0);
                    yield return new WaitForSeconds(0.01f);
                }
            }
            else
            {
                for (float i = _currentRotation; i > ClosedRotation; i--)
                {
                    _currentRotation = i;
                    _pivot.eulerAngles = new Vector3(0, _currentRotation, 0);
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }

        ChangeDoorState(); // Cambiamos el estado de la puerta
    }

    // METODO PARA CAMBIAR EL ESTADO DE LA PUERTA
    public void ChangeDoorState() 
    {
        IsOpen = !IsOpen;

        _onlyOnce = true; // La corrutina terminó, el enemigo puede volver a cerrar la puerta si estA abierta
    }

    // METODO PARA INTENTAR ABRIR LA PUERTA (En funciOn del tipo de puzle)
    public void TryOpen(GameObject player)
    {

        if (Type != Puzzle.Type.None) _audioSource.PlayOneShot(_doorUnlockedSound);
        Debug.Log($"Abriendo puerta tipo {Type}");


        if (Type == Puzzle.Type.None)  // Se puede abrir y cerrar la puerta sin mas
        {
            StartCoroutine(OpenClose(IsOpen));
        } else if (Type == Puzzle.Type.Object) // Se necesita un objeto en el inventario para abrir
        {
            InventoryItem item;
            if (CheckIfPlayerHasObject(out item)) 
            {
                InventorySystem.Instance.Remove(item.Data);
            }
        } else if (Type == Puzzle.Type.OneWay)  // Por definir
        {
            if (player.transform.position.x > _pivot.position.x) ChangeDoorType(Puzzle.Type.None);
        }
    }

    // METODO PARA COMPROBAR SI EL JUGADOR POSEE UN OBJETO PARA PASAR POR LA PUERTA
    private bool CheckIfPlayerHasObject(out InventoryItem item) 
    {
        bool hasKey = false;
        item = null;

        foreach (InventoryItem invItem in InventorySystem.Instance.Inventory) // Se comprueban los objetos del inventario
        {
            if (this.Id == "door_01" && invItem.Data.Id == UnlockObject.Id)
            {
                hasKey = true; 
                item = invItem;
                ChangeDoorType(Puzzle.Type.None);
                break;
            }
        }

        return hasKey;
    }

    // METODO PARA CAMBIAR EL TIPO DE PUERTA EN FUNCION DEL TIPO DE PUZLE
    private void ChangeDoorType(Puzzle.Type newType) 
    {
        this.Type = newType;
    }

    // ANTIGUO OPEN CLOSE - NO BORRAR POR SI ACASO
    // public void OpenClose(bool isOpen) {

    //     if (IsInverted) {
    //         if (isOpen == true && currentRotation <= openedRotation) {
    //             currentRotation+=1f;
    //         } else if (isOpen == false && currentRotation > closedRotation) {
    //             currentRotation-=1f;
    //         }
    //     } else {
    //         if (isOpen == true && currentRotation >= openedRotation) {
    //             currentRotation-=1f;
    //         } else if (isOpen == false && currentRotation < closedRotation) {
    //             currentRotation+=1f;
    //         }
    //     }

    //     pivot.eulerAngles = new Vector3(0, currentRotation, 0);
    // }

}




