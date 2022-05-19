using System.Collections.Specialized;
using UnityEngine;
using UnityEditor;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [SerializeField] private GameObject mSlotPrefab;
    [SerializeField] private GameObject parentPanel; // El padre de la UI de Inventario
    [SerializeField] private GameObject combinationToDo; // El padre de la UI de Inventario
    

    void Start()
    {
        
        InventorySystem.Instance.inventory.CollectionChanged += OnUpdateInventory; // esta linea tiene que llamarse despues de que cargue InventorySystem
    }

    void Update()
    {
        CheckCombinations();
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
        CheckCombinations();
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

    public void CheckCombinations() {

        Debug.Log("CHQEUEA");

        if (InventorySystem.Instance.FindById("dinning_room_key")!=null && InventorySystem.Instance.FindById("salt_pinch")!=null) {
            Debug.Log("Receta buena");
            // elminiar ambos ingredientes
            InventorySystem.Instance.RemoveById("dinning_room_key");
            InventorySystem.Instance.RemoveById("salt_pinch");
            
            // añadir el nuevo
            InventorySystem.Instance.AddById("dinning_room_salted_key");
            
            
        }

        // Plantilla recetas
        // if (InventorySystem.Instance.Find("ingrediente1")!=null && InventorySystem.Instance.Find("ingrediente2")!=null)

        

        

    }

}
