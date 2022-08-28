/* SCRIPT QUE CONTROLA EL FUNCIONAMIENTO DEL MENU DEL INVENTARIO */

using System.Collections.Specialized;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    /* VARIABLES */

    // REFERENCIAS 

    public static Inventory Instance;

    [SerializeField] private GameObject _slotPrefab;  // Prefab de ranura
    [SerializeField] private GameObject _parentPanel;  // El padre de la UI de Inventario
    [SerializeField] private GameObject _combinationToDo;  // CombinaciOn a hacer

    // TIMERS
    private float _timeLeft = 3;  // Timer para chequear las comprobaciones

    // FLAGS
    private bool _crafted = false;
    
    /* METODOS BASICOS */

    // METODO START
    // Start es un mEtodo llamado antes del primer frame
    private void Start()
    {
        InventorySystem.Instance.Inventory.CollectionChanged += OnUpdateInventory; // Esta linea tiene que llamarse despues de que cargue InventorySystem
    }

    // METODO UPDATE
    // Update es un mEtodo llamado en cada frame
    private void Update()
    {
        _timeLeft -= Time.deltaTime;
        if ( _timeLeft < 0 )
        {
            CheckCombinations();
            _timeLeft = 3;
        }
        
    }

    /* METODOS DEL MENU DE INVENTARIO */

    // METODO PARA SALIR DEL INVENTARIO
    public void BackToGame() 
    {
        GameManager.Instance.ChangeState(GameState.Game);
    }

    // METODO DE ACTUALIZACION DEL INVENTARIO
    public void OnUpdateInventory(object sender, NotifyCollectionChangedEventArgs e) 
    {

        // SE BORRA EL INVENTARIO
        foreach (Transform t in _parentPanel.transform)
        {
            Destroy(t.gameObject);
        }

        // SE REDIBUJA EL INVENTARIO
        DrawInventory();
        CheckCombinations();
    }

    // METODO PARA DIBUJAR EL INVENTARIO
    public void DrawInventory() {

        // SE DIBUJAN TODOS LOS OBJETOS UNO A UNO
        foreach (InventoryItem item in InventorySystem.Instance.Inventory)
        {
            AddInventorySlot(item);
        }

    }

    // METODO PARA AÑADIR UN RANURA AL INVENTARIO A PARTIR DE UN NUEVO OBJETO
    public void AddInventorySlot(InventoryItem item) 
    {

        // Se crea la ranura
        GameObject obj = Instantiate(_slotPrefab);
        obj.transform.SetParent(_parentPanel.transform, false);

        // Se aniade el objeto
        ItemSlot slot = obj.GetComponent<ItemSlot>();
        slot.Set(item);

    }

    // METODO PARA REALIZAR COMBINACIONES Y SUS COMPROBACIONES
    public void CheckCombinations() 
    {

        // LLAVE + SAL
        if (InventorySystem.Instance.FindById("dinning_room_key")!=null 
            && InventorySystem.Instance.FindById("salt_pinch")!=null) 
        {

            // Debug.Log("Receta buena");

            // SE ELIMINAN AMBOS INGREDIENTES
            InventorySystem.Instance.RemoveById("dinning_room_key");
            InventorySystem.Instance.RemoveById("salt_pinch");
            
            // SE ALADE EL OBJETO RESULTANTE
            GameObject newItem = Resources.Load<GameObject>("Items/Dinning Room Salted Key");
            
            // SI TODAVIA NO EXISTE EL NUEVO OBJETO SE INDICA QUE NO SE HA CREADO
            if (!newItem)
            {
                _crafted = false;
                return;
            }

            // SI EL NUEVO OBJETO NO SE HA CREADO, SE CREA
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
