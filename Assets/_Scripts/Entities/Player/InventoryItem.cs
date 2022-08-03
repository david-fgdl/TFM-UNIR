using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public InventoryItemData Data { get; private set; } // Inventory items data
    public int StackSize { get; private set; } // Inventory size

    public InventoryItem(InventoryItemData source) 
    {
        Data = source;
        AddToStack();
    }

    public void AddToStack() // Actualize stack size
    {
        StackSize++;
    }

    public void RemoveFromStack() 
    {
        StackSize--;
    }
}
