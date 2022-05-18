using System.Collections.Specialized;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [SerializeField] private GameObject mSlotPrefab;
    // Start is called before the first frame update

    void Awake() // deberia llamarse incluso con el objeto disabled.
    {
        Debug.Log("AWAKE");
        InventorySystem.Instance.inventory.CollectionChanged += OnUpdateInventory;
        // Da problema porque inventory está desabilitado cuando no se muestra
        // Se podría arreglar cambiando esta inicializacion en un gameobject externo
    }

    public void BackToGame() {
        //Utils.BetterLog(this.GetType().Name, "BackToGame", "VUelta al juego");
        GameManager.Instance.ChangeState(GameState.Game);
    }

    public void OnUpdateInventory(object sender, NotifyCollectionChangedEventArgs e) {
        foreach (Transform t in transform)
        {
            Debug.Log("Destryo?");
            Destroy(t.gameObject);
        }

        DrawInventory();
    }

    public void DrawInventory() {
        foreach (InventoryItem item in InventorySystem.Instance.inventory)
        {
            Debug.Log("Add to UI Inventory: "+item.data.DisplayName);
            AddInventorySlot(item);
        }
    }

    public void AddInventorySlot(InventoryItem item) {
        GameObject obj = Instantiate(mSlotPrefab);
        obj.transform.SetParent(transform, false);

        ItemSlot slot = obj.GetComponent<ItemSlot>();
        slot.Set(item);
    }
}
