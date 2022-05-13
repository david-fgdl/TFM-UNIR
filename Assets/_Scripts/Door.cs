using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        OpenClose(IsOpen, IsInverted);
    }

    void OpenClose(bool isOpen, bool isInverted) {

        if (isInverted) {
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
    
}




