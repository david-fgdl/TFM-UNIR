using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject mSlotPrefab;
    // Start is called before the first frame update
    void Start()
    {
        InventorySystem.current.onInventoryChangedEvent += OnUpdateInventory;
    }

    public void BackToGame() {
        GameManager.Instance.ChangeState(GameState.Game);
    }

    private void OnUpdateInventory() {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        DrawInventory();
    }

    public void DrawInventory() {
        foreach (InventoryItem item in InventorySystem.current.inventory)
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
