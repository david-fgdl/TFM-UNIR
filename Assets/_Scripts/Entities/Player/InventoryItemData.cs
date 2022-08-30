/* SCRIPT PARA ALMACENAR LA INFORMACION DE UN DETERMINADO OBJETO */

using UnityEngine;

[CreateAssetMenu(menuName = "TFM-UNIR/New Item Data")]
public class InventoryItemData : ScriptableObject
{

    /* VARIABLES */

    // PARAMETROS
    public string Id;
    public string DisplayName;
    public Sprite Icon;
    public GameObject Prefab;
    public bool IsCombinable;
}
