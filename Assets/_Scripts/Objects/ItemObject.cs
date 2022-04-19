using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;

    public void OnHandlePickupItem() {
        InventorySystem.Instance.Add(referenceItem);
        Destroy(gameObject);
    }
}
