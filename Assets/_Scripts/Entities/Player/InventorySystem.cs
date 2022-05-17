using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    private Dictionary<InventoryItemData, InventoryItem> m_itemDictionary;
    public ObservableCollection<InventoryItem> inventory { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        inventory = new ObservableCollection<InventoryItem>();
        m_itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
    }
    
    public void Add(InventoryItemData referenceData) {
        
        if (m_itemDictionary.TryGetValue(referenceData, out InventoryItem value)) {
            value.AddToStack();
        }
        else {
            InventoryItem newItem = new InventoryItem(referenceData);
            inventory.Add(newItem);
            m_itemDictionary.Add(referenceData, newItem);
        }

        

        
    }

    public void Remove(InventoryItemData referenceData) {
        if (m_itemDictionary.TryGetValue(referenceData, out InventoryItem value)) {
            value.RemoveFromStack();

            if (value.stackSize == 0) {
                inventory.Remove(value);
                m_itemDictionary.Remove(referenceData);
            }
        }
    }

}
