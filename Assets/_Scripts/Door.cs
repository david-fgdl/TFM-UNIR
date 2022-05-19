using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CLASE Door
// Clase para definir los aspectos de las puertas, 
// que son necesarias para pasar de zona a zona.
public class Door : MonoBehaviour {

    public string Id;
    public string DoorName;
    public InventoryItemData UnlockObject;
    public Puzzle.Type Type;
    

    #region Open Variables
    public bool IsOpen = false;
    public bool IsInverted = false;
    public float closedRotation = 0f;
    public float openedRotation = -87f;
    [SerializeField] private Transform pivot;
    
    private float currentRotation;
    

    #endregion

    void Awake()
    {
        
    }

    void Start()
    {
        currentRotation = closedRotation;
    }

    void Update()
    {
         
    }

    public void OpenClose(bool isOpen) {

        if (IsInverted) {
            if (isOpen == true) {
                for (float i = currentRotation; currentRotation <= openedRotation; i++) {
                    pivot.eulerAngles = new Vector3(0, currentRotation, 0);
                }
            } else if (isOpen == false) {
                for (float i = currentRotation; currentRotation > closedRotation; i--) {
                    pivot.eulerAngles = new Vector3(0, currentRotation, 0);
                }
            }
        } else {
            if (isOpen == true) {
                for (float i = currentRotation; currentRotation >= openedRotation; i--) {
                    pivot.eulerAngles = new Vector3(0, currentRotation, 0);
                }
            } else if (isOpen == false) {
                for (float i = currentRotation; currentRotation < closedRotation; i++) {
                    pivot.eulerAngles = new Vector3(0, currentRotation, 0);
                }
            }
        }

        ChangeDoorState();
    }

    public void ChangeDoorState() {
        if (!IsOpen)
            IsOpen = true;
        else 
            IsOpen = false;
    }

    public void TryOpen() {
        if (Type == Puzzle.Type.None) {
            // Se puede abrir y cerrar la puerta sin mas
            OpenClose(IsOpen);
        } else if (Type == Puzzle.Type.Object) {
            // Se necesita un objeto en el inventario para abrir
            InventoryItem item;
            if (CheckIfPlayerHasObject(out item)) {
                // abrir???? igual no hace falta
                // eliminar objeto
                InventorySystem.Instance.Remove(item.data);
            }

        }
    }

    private bool CheckIfPlayerHasObject(out InventoryItem item) {
        bool hasKey = false;
        item = null;

        foreach (InventoryItem invItem in InventorySystem.Instance.inventory)
        {
            if (this.Id == "door_01" && invItem.data.Id == UnlockObject.Id)
            {
                hasKey = true; 
                item = invItem;
                ChangeDoorType(Puzzle.Type.None);
                break;
            }
        }

        return hasKey;
    }

    private void ChangeDoorType(Puzzle.Type newType) {
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




