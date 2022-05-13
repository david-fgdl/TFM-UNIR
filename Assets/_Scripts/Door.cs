using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TFM-UNIR/New Door")]
public class Door : ScriptableObject {

    public string Id;
    public string DoorName;
    public string UnlockObjectId;
    public Puzzle.Type Type;
    public GameObject Prefab;
    public bool IsOpen;
    
}




