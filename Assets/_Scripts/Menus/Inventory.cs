using System.Collections.Specialized;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject mSlotPrefab;
    // Start is called before the first frame update
    void OnLevelWasLoaded() // El constructor se va a llamar incluso con el objeto disabled.
    {
        InventorySystem.Instance.inventory.CollectionChanged += OnUpdateInventory;

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
