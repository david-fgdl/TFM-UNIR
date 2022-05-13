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
    [SerializeField] private Transform pivot;
    private float closedRotation = 0f;
    private float currentRotation;
    private float openedRotation = -87f;

    #endregion

    void Awake()
    {
        this.gameObject.name = Id;
    }

    void Start()
    {
        currentRotation = closedRotation;
    }

    void Update()
    {
        OpenClose(IsOpen);
    }

    void OpenClose(bool isOpen) {

        
        if (isOpen == true && currentRotation >= openedRotation) {
            currentRotation-=1f;
        } else if (isOpen == false && currentRotation < closedRotation) {
            currentRotation+=1f;
        }

        pivot.eulerAngles = new Vector3(0, currentRotation, 0);
    }
    
}




