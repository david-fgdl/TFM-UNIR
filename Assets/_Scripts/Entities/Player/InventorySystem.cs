/* SCRIPT PARA CONTROLAR EL COMPORTAMIENTO DEL SISTEMA DE INVENTARIO */

using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    // INSTANCIA DE LA CLASE
    public static InventorySystem Instance;

    // VARIABLES PARA ALMACENAR LAS COLECCIONES
    private Dictionary<InventoryItemData, InventoryItem> _itemDictionary;
    public ObservableCollection<InventoryItem> Inventory { get; private set; }

    /* METODOS BASICOS */

    // METODO AWAKE
    private void Awake()
    {

        // CREACION DE LA INSTANCIA
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        // INICIALIZACION DE COLECCIONES
        Inventory = new ObservableCollection<InventoryItem>();
        _itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();

    }

    /* METODOS DE INVENTARIO */
    
    // METODO PARA AÑADIR OBJETOS AL INVENTARIO
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

    // METODO PARA BORRAR OBJETOS DEL INVENTARIO
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

    // METODO PARA ENCONTRAR OBJETOS DENTRO DEL INVENTARIO EN FUNCION DE UN ID RECIBIDO COMO PARAMETRO
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

    // METODO PARA BORRAR OBJETOS DEL INVENTARIO EN FUNCION DE UN ID RECIBIDO COMO PARAMETRO
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
