using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    private Dictionary<InventoryItemData, InventoryItem> _itemDictionary;
    public ObservableCollection<InventoryItem> Inventory { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        Inventory = new ObservableCollection<InventoryItem>();
        _itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();

    }
    
    public void Add(InventoryItemData referenceData) 
    {
        
        if (_itemDictionary.TryGetValue(referenceData, out InventoryItem value)) 
        {
            value.AddToStack();
        }
        else 
        {
            InventoryItem newItem = new InventoryItem(referenceData);
            Inventory.Add(newItem);
            _itemDictionary.Add(referenceData, newItem);
        }
    }

    public void Remove(InventoryItemData referenceData) 
    {
        if (_itemDictionary.TryGetValue(referenceData, out InventoryItem value)) 
        {
            value.RemoveFromStack();

            if (value.StackSize == 0) 
            {
                Inventory.Remove(value);
                _itemDictionary.Remove(referenceData);
            }
        }
    }

    public InventoryItem FindById(string itemId) {

        foreach (var item in _itemDictionary)
        {
            if (_itemDictionary.TryGetValue(item.Key, out InventoryItem value) && item.Key.Id == itemId)
            {
                return value;
            }
        }

        return null;
    }

    public void RemoveById(string itemId) {

        foreach (var item in _itemDictionary)
        {
            if (_itemDictionary.TryGetValue(item.Key, out InventoryItem value) && item.Key.Id == itemId)
            {
                Remove(item.Key);
                break;
            }
        }
    }

}
