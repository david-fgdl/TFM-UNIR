using System.Collections.Specialized;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject mSlotPrefab;
    // Start is called before the first frame update
    void Start()
    {
        InventorySystem.Instance.inventory.CollectionChanged += OnUpdateInventory;
    }

    public void BackToGame() {
        GameManager.Instance.ChangeState(GameState.Game);
    }

    private void OnUpdateInventory(object sender, NotifyCollectionChangedEventArgs e) {
        foreach (Transform t in transform)
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
        obj.transform.SetParent(transform, false);

        ItemSlot slot = obj.GetComponent<ItemSlot>();
        slot.Set(item);
    }
}
