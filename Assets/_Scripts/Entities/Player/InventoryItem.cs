/* SCRIPT PARA CONTROLAR EL COMPORTAMIENTO DE LOS OBJETOS */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{

    /* VARIABLES */

    // ARRAYS PARA ALMACENAR INFORMACION DE LOS OBJETOS
    public InventoryItemData Data { get; private set; }
    public int StackSize { get; private set; }

    /* METODOS DE LOS OBJETOS */

    // METODO PARA ASIGNAR LA INFORMACION DEL OBJETO
    public InventoryItem(InventoryItemData source) 
    {
        Data = source;
        AddToStack();
    }

    // METODO PARA INDICAR EL INCREMENTO DE OBJETOS
    public void AddToStack()
    {
        StackSize++;
    }

    // METODO PARA INDICAR EL DECREMENTO DE OBJETOS
    public void RemoveFromStack() 
    {
        StackSize--;
    }

}
