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
    

    #region Open Variables
    public bool IsOpen = false; // Estado de la puerta
    public bool IsInverted = false; // Marca dónde están ubicadas las bisagras
    public float ClosedRotation; // Rotacion de la puerta cerrada
    public float OpenedRotation; // Rotacion de la puerta abierta
    [SerializeField] private Transform _pivot; // Posición de la bisagra
    
    private float _currentRotation; // Rotación actual de la puerta
    

    #endregion


    private void Start()
    {
        _currentRotation = ClosedRotation;
    }

    IEnumerator OpenClose(bool isOpen) 
    {

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
        if (!IsOpen)
            IsOpen = true;
        else 
            IsOpen = false;
    }

    public void TryOpen() 
    {
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




