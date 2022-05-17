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
        if (Type == Puzzle.Type.None) {
            OpenClose(IsOpen);
        }
            
    }

    public void OpenClose(bool isOpen) {

        if (IsInverted) {
            if (isOpen == true && currentRotation <= openedRotation) {
                currentRotation+=1f;
            } else if (isOpen == false && currentRotation > closedRotation) {
                currentRotation-=1f;
            }
        } else {
            if (isOpen == true && currentRotation >= openedRotation) {
                currentRotation-=1f;
            } else if (isOpen == false && currentRotation < closedRotation) {
                currentRotation+=1f;
            }
        }
        
        pivot.eulerAngles = new Vector3(0, currentRotation, 0);
    }

    public void ChangeDoorState() {
        if (!IsOpen)
            IsOpen = true;
        else 
            IsOpen = false;
    }

    // public void OpenClose(bool isOpen) {

    //     for (float i = currentRotation; currentRotation <= openedRotation; currentRotation++) {
            
    //     }

    //     while (currentRotation <= openedRotation) {
    //         currentRotation+=1f;
    //         pivot.eulerAngles = new Vector3(0, currentRotation, 0);
    //     }
        


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




