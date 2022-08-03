using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData ReferenceItem;

    public void OnHandlePickupItem() 
    {
        InventorySystem.Instance.Add(ReferenceItem);
        Destroy(gameObject);
    }
}
