using System.Collections.Specialized;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [SerializeField] private GameObject mSlotPrefab;
    [SerializeField] private GameObject parentPanel; // El padre de la UI de Inventario
    

    void Start()
    {
        
        InventorySystem.Instance.inventory.CollectionChanged += OnUpdateInventory; // esta linea tiene que llamarse despues de que cargue InventorySystem
    }

    public void BackToGame() {
        //Utils.BetterLog(this.GetType().Name, "BackToGame", "VUelta al juego");
        GameManager.Instance.ChangeState(GameState.Game);
    }

    public void OnUpdateInventory(object sender, NotifyCollectionChangedEventArgs e) {
        foreach (Transform t in parentPanel.transform)
        {
            Destroy(t.gameObject);
        }

        DrawInventory();
    }

    public void DrawInventory() {
        foreach (InventoryItem item in InventorySystem.Instance.inventory)
        {
            AddInventorySlot(item);
        }
    }

    public void AddInventorySlot(InventoryItem item) {
        GameObject obj = Instantiate(mSlotPrefab);
        obj.transform.SetParent(parentPanel.transform, false);

        ItemSlot slot = obj.GetComponent<ItemSlot>();
        slot.Set(item);
    }
}
