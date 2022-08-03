using UnityEngine;

[CreateAssetMenu(menuName = "TFM-UNIR/New Item Data")]
public class InventoryItemData : ScriptableObject
{
    public string Id;
    public string DisplayName;
    public Sprite Icon;
    public GameObject Prefab;
    public bool IsCombinable;
}
