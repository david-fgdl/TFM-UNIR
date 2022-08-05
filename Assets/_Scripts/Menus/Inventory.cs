using System.Collections.Specialized;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameObject _parentPanel; // El padre de la UI de Inventario
    [SerializeField] private GameObject _combinationToDo;
    private float _timeLeft = 3;

    private bool _crafted = false;
    

    private void Start()
    {
        InventorySystem.Instance.Inventory.CollectionChanged += OnUpdateInventory; // esta linea tiene que llamarse despues de que cargue InventorySystem
    }

    private void Update()
    {
        _timeLeft -= Time.deltaTime;
        if ( _timeLeft < 0 )
        {
            CheckCombinations();
            _timeLeft = 3;
        }
        
    }

    public void BackToGame() 
    {
        GameManager.Instance.ChangeState(GameState.Game);
    }

    public void OnUpdateInventory(object sender, NotifyCollectionChangedEventArgs e) 
    {
        foreach (Transform t in _parentPanel.transform)
        {
            Destroy(t.gameObject);
        }

        DrawInventory();
        CheckCombinations();
    }

    public void DrawInventory() {
        foreach (InventoryItem item in InventorySystem.Instance.Inventory)
        {
            AddInventorySlot(item);
        }
    }

    public void AddInventorySlot(InventoryItem item) 
    {
        GameObject obj = Instantiate(_slotPrefab);
        obj.transform.SetParent(_parentPanel.transform, false);

        ItemSlot slot = obj.GetComponent<ItemSlot>();
        slot.Set(item);
    }

    public void CheckCombinations() 
    {
        if (InventorySystem.Instance.FindById("dinning_room_key")!=null 
            && InventorySystem.Instance.FindById("salt_pinch")!=null) 
        {
            // Debug.Log("Receta buena");
            // elminiar ambos ingredientes
            InventorySystem.Instance.RemoveById("dinning_room_key");
            InventorySystem.Instance.RemoveById("salt_pinch");
            
            // añadir el nuevo
            GameObject newItem = Resources.Load<GameObject>("Items/Dinning Room Salted Key");
            
            if (!newItem)
            {
                _crafted = false;
                return;
            }

            if (!_crafted) 
            {
                Debug.Log("Aqui tienes");
                Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
                Instantiate(newItem, new Vector3(playerPosition.x, playerPosition.y, playerPosition.z + 2), newItem.transform.rotation);
                
            }

            _crafted = true;
            return;
        }

        // Plantilla recetas
        // if (InventorySystem.Instance.Find("ingrediente1")!=null && InventorySystem.Instance.Find("ingrediente2")!=null)

        

        

    }

}
