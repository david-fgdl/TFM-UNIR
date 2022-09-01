using System.Collections;
using UnityEngine;

// CLASE Door
// Clase para definir los aspectos de las puertas, 
// que son necesarias para pasar de zona a zona.
public class Door : MonoBehaviour {

    public string Id; // Id de la puerta
    public string DoorName; // Nombre de la puerta
    public InventoryItemData UnlockObject; // Objeto que desbloquea la puerta
    public Puzzle.Type Type; // Tipo de puzzle
    private GameObject _enemyRef; //Referencia del enemigo
    private bool _onlyOnce; //Switch para evitar muchas ejecuciones
    

    #region Open Variables
    public bool IsOpen = false; // Estado de la puerta
    public bool IsInverted = false; // Marca dónde están ubicadas las bisagras
    public float ClosedRotation; // Rotacion de la puerta cerrada
    public float OpenedRotation; // Rotacion de la puerta abierta
    #endregion


    [SerializeField] private Transform _pivot; // Posición de la bisagra
    private float _currentRotation; // Rotación actual de la puerta

    // VARIABLES DE SONIDO
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _doorUnlockedSound; // Sonido de Sound Effect from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=music&amp;utm_content=95884">Pixabay</a>
    [SerializeField] private AudioClip _doorOpeningSound; // Sonido modificado de Sound Effect from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=music&amp;utm_content=43633">Pixabay</a>
    [SerializeField] private AudioClip _doorClosingSound; // Sonido modificado de Sound Effect from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=music&amp;utm_content=43633">Pixabay</a>

    private void Start()
    {
        _currentRotation = ClosedRotation;
        _enemyRef = GameObject.FindGameObjectWithTag("Enemy");
        _onlyOnce = true;
    }

    private void Update()
    {
        //Si la puerta esta abierta y el enemigo pasa por mi lado y no acabo de ejecutar la corrutina o ya termino la corrutina
        if (IsOpen && Vector3.Distance(transform.position, _enemyRef.transform.position) < 3 && _onlyOnce)
        {
            _onlyOnce = false;
            StartCoroutine(OpenClose(IsOpen)); // Cierra la puerta
        }
    }

    private IEnumerator OpenClose(bool isOpen) 
    {

        if (!IsOpen) _audioSource.PlayOneShot(_doorOpeningSound);
        else _audioSource.PlayOneShot(_doorClosingSound);

        if (!IsInverted) 
        { // Comprobamos si está invertida
            if (!IsOpen) // Comprobamos si esta cerrada
            {
                for (float i = _currentRotation; i >= OpenedRotation; i--)
                { // open not inverted ex.: door_00
                    _currentRotation = i;
                    _pivot.eulerAngles = new Vector3(0, _currentRotation, 0);
                    yield return new WaitForSeconds(0.01f);
                }
            }
            else
            {
                for (float i = _currentRotation; i < ClosedRotation; i++)
                { // close not inverted ex.: door_00
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
                { // open not inverted ex.: door_01
                    _currentRotation = i;
                    _pivot.eulerAngles = new Vector3(0, _currentRotation, 0);
                    yield return new WaitForSeconds(0.01f);
                }
            }
            else
            {
                for (float i = _currentRotation; i > ClosedRotation; i--)
                { // close not inverted ex.: door_01
                    _currentRotation = i;
                    _pivot.eulerAngles = new Vector3(0, _currentRotation, 0);
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }

        ChangeDoorState(); // Cambiamos el estado de la puerta
    }

    public void ChangeDoorState() 
    {
        IsOpen = !IsOpen;

        _onlyOnce = true; //Lo ultimo de la corrutina terminó, el enemigo puede volver a cerrar si esta abierta
    }

    public void TryOpen(GameObject player) // Aqui se añaden los tipos de puerta
    {
        if (Type != Puzzle.Type.None) _audioSource.PlayOneShot(_doorUnlockedSound);

        Debug.Log($"Abriendo puerta tipo {Type}");


        //? Estos ifs se puede cambiar por un switch case
        if (Type == Puzzle.Type.None) 
        { // Se puede abrir y cerrar la puerta sin mas
            StartCoroutine(OpenClose(IsOpen));
        } else if (Type == Puzzle.Type.Object) 
        {
            // Se necesita un objeto en el inventario para abrir
            InventoryItem item;
            if (CheckIfPlayerHasObject(out item)) 
            {
                InventorySystem.Instance.Remove(item.Data);
            }
        } else if (Type == Puzzle.Type.OneWay)
        {
            if (player.transform.position.x > _pivot.position.x) ChangeDoorType(Puzzle.Type.None);
        }
    }

    private bool CheckIfPlayerHasObject(out InventoryItem item) 
    {
        bool hasKey = false;
        item = null;

        foreach (InventoryItem invItem in InventorySystem.Instance.Inventory) // Comprobamos los objetos en  nuestro inventario
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




