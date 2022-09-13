/* SCRIPT QUE CONTROLA LA ADICION DE OBJETOS AL INVENTARIO */

using UnityEngine;

public class ItemObject : MonoBehaviour
{

    /* VARIABLES */

    // REFERENCIAS
    public InventoryItemData ReferenceItem;

    /* METODOS DEL OBJETO */

    // METODO PARA AÑADOR EL OBJETO AL INVENTARIO
    public void OnHandlePickupItem() 
    {
        InventorySystem.Instance.Add(ReferenceItem);
        Destroy(gameObject);
    }
}
