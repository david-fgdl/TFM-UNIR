/* SCRIPT TO CONTROL PLAYER'S BEHAVIOUR */
/*-----------------------------------------------------------------------------------------------------------------------------*/

using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{

/*-----------------------------------------------------------------------------------------------------------------------------*/

    /* VARIABLES */

    // STATS DEL JUGADOR
    [Header("Player Stats")]
    private int maxSaltAmount = 100;
    private int currentSaltAmount;
    public SaltPouch saltPouch;
    [SerializeField] private int _saltThrowDistance;
    [SerializeField] private int _amountSaltLostByDamage;

    // VELOCIDAD DE MOVIMIENTO DEL JUGADOR
    [Header("Player Movement Speed")]
    [SerializeField] [Range(1, 20)] private float _speed = 2.5f;

    // SENSIBILIDAD DEL CURSOR
    [Header("Mouse sensitivity")]
    [SerializeField] [Range(1, 100)] private float mouseSensivity = 50;

    // REFERENCIAS

    [SerializeField] private Camera playerCamera;

    private CharacterController characterController;
    private Animator animator;

    private Vector3 playerPreviousPosition;

    #region Player Input

        private PlayerInput playerInput;

        private InputAction moveAction;
        private InputAction lookAction;
        private InputAction grabAction;
        private InputAction inventoryAction;
        private InputAction throwAction;

    #endregion

    #region Audio

        [Header("Audio Clips")]

        [SerializeField] private AudioClip breathingSound;
        [SerializeField] private AudioClip walkingSound;
        [SerializeField] private AudioClip standartMusic;

    #endregion

    // ENEMIGO
    
    private GameObject _enemyRef;
    private EnemyHealthSystem _enemyHealthRef;
    [SerializeField] private int _damageRange;//Distance the enemy can damage the player
    [SerializeField] private int _damageByThrownSalt;//Damage the enemy takes by the thrown salt
    [SerializeField] private int _damageByDamage;//Damage the enemy takes by dealing damage

    // VARIABLES AUXILIARES

    private float xRotation;  // Variable para conocer la posiciOn de la cAmara en el eje x

    private string _selectableTag = "Selectable";
    private Transform _selection;
    private bool _canDamageCoroutine = true;
    Coroutine _routine;

    /* METODOS BASICOS */

    // METODO AWAKE
    private void Awake()
    {

        // ESTABLECER LAS REFERENCIAS

        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        grabAction = playerInput.actions["Grab"];
        inventoryAction = playerInput.actions["P_Inventory"];
        throwAction = playerInput.actions["Fire"];

        animator = GetComponent<Animator>();

        characterController = GetComponent<CharacterController>();
        if (characterController==null) Debug.Log("CHARACTER CONTROLLER ES NULL");

    }

    // METODO START
    // Start es un mEtodo llamado antes del primer frame
    private void Start()
    {

        playerPreviousPosition = transform.position;  // La posiciOn de comienzo se guarda como la posiciOn anterior

        // FIJAR VALORES DE SAL
        currentSaltAmount = maxSaltAmount;
        saltPouch.SetMaxSaltAmount(maxSaltAmount);

        // FIJAR REFERENCIAS DEL ENEMIGO
        _enemyRef = GameObject.FindGameObjectWithTag("Enemy");
        _enemyHealthRef = _enemyRef.GetComponent<EnemyHealthSystem>();

    }

    // METODO UPDATE
    // Update es llamado una vez por frame
    private void Update()
    {
 
        // MOVIMIENTO BASICO
        Move();
        Look();
        

        // GESTION DE LA INTERACCION CON EL ENEMIGO
        if (Vector3.Distance(transform.position, _enemyRef.transform.position) < _damageRange && _canDamageCoroutine)
        {
            _canDamageCoroutine = false;
            _routine = StartCoroutine(getDamageRoutine());
            
        }
        else
        {
            if(!(Vector3.Distance(transform.position, _enemyRef.transform.position) < _damageRange))
            {
                StopCoroutine(_routine);
                _canDamageCoroutine = true;
            }
            
        }

    } 

    /* EVENTOS DE ACTIVACION Y DESACTIVACION DEL SCRIPT */

    // METODO DE ACTIVACION
    // Es llamado cuando se activa el script
    private void OnEnable()
    {
        playerInput.enabled = true;
        grabAction.performed += Grab;
        inventoryAction.performed += Inventory;
        throwAction.performed += ThrowSalt;
    }

    // METODO DE DESACTIVACION
    // Es llamado cuando se desactiva el script
    private void OnDisable()
    {
        playerInput.enabled = false;
        grabAction.performed -= Grab;
        inventoryAction.performed -= Inventory;
        throwAction.performed -= ThrowSalt;
    }

    /* METODOS DE MOVIMIENTO*/

    // METODO PARA CONTROLAR EL DESPLAZAMIENTO DEL JUGADOR
    private void Move()
    {

        Vector3 moveVector = transform.right * moveAction.ReadValue<Vector2>().x + transform.forward * moveAction.ReadValue<Vector2>().y;
        characterController.Move(moveVector * Time.deltaTime * _speed);

        // GESTOR DE ANIMACIONES
        if (Vector3.Distance(playerPreviousPosition, gameObject.transform.position) <= 0.05f)  animator.SetBool("is_walking", false);
        else  animator.SetBool("is_walking", true);

        // GUARDAR LA POSICION ACTUAL COMO LA POSICION ANTERIOR
        playerPreviousPosition = gameObject.transform.position;


    }

    // METODO PARA CONTROLAR EL MOVIMIENTO DE LA CAMARA
    private void Look()
    {

        // OBTENER VALORES DELTA DEL RATON
        float mouseX = lookAction.ReadValue<Vector2>().x * mouseSensivity * Time.deltaTime;
        float mouseY = lookAction.ReadValue<Vector2>().y * mouseSensivity * Time.deltaTime;

        // ESTABLECER Y DELIMITAR LA ROTACION EN EL EJE X
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        // ESTABLECER LA ROTACION EN EL EJE Y
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // ROTAR EN EL EJE X
        this.transform.Rotate(Vector3.up * mouseX);

    }

    /* METODOS DE INTERACCION CON OBJETOS */

    // METODO DE SELECCION DE OBJETOS
    private void CheckObject() {

        
        if (_selection != null) 
        {
            _selection = null;
        }
        
        // SELECCION DEL OBJETO MEDIANTE RAYCAST

        var ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10f)) 
        {
            var selection = hit.transform;
            
            if (selection.name.Contains("Door")) 
            {
                Door door = selection.GetComponentInParent<Door>();
                door.TryOpen(gameObject);
                _selection = selection;
            }

            if (selection.CompareTag(_selectableTag))
            {            
                if (selection.TryGetComponent<ItemObject>(out ItemObject item)) 
                {
                    item.OnHandlePickupItem();
                }

                _selection = selection;
            }
        }
    }

    // METODO PARA AGARRAR OBJETOS
    public void Grab(InputAction.CallbackContext context)
    {
        if (context.started && !animator.GetBool("can_grab")) 
        {

            // CHEQUEO Y GESTION DE ANIMACIONES
            animator.SetBool("can_grab", true);
            CheckObject();
            animator.SetBool("can_grab", false);

            // EL OBJETO VA AL INVENTARIO (OTRO SCRIPT)
            // EL OBJETO SE DESTRUYE PERO ES ALMACENADO EN EL ARRAY DEL INVENTARIO
        }
    }

    // METODO DE APERTURA / CIERRE DEL INVENTARIO
    public void Inventory(InputAction.CallbackContext context)
    {
        bool performed = false;

        if (context.started && !performed) 
        {

            // GESTOR DE ESTADOS DEL JUEGO
            if (GameManager.Instance.State == GameState.Game)
                GameManager.Instance.ChangeState(GameState.Inventory);
            else if (GameManager.Instance.State == GameState.Inventory)
                GameManager.Instance.ChangeState(GameState.Game);

            performed = true;
        }

        performed = false;
    }

    /* METODOS DE INTERACCION CON EL ENEMIGO */

    // METODO PARA DISPARAR SAL AL ENEMIGO
    public void ThrowSalt(InputAction.CallbackContext context)
    {
        Debug.Log("Salt throw!");
        var saltAmount = 10;

        if (currentSaltAmount > 0) {

            // FIJAR CANTIDAD DE SAL
            currentSaltAmount -= saltAmount;
            saltPouch.SetSaltAmount(currentSaltAmount);

            // CONTROLAR DISPARO
            if (Vector3.Distance(transform.position, _enemyRef.transform.position) < _saltThrowDistance)
            {
                _enemyHealthRef.LoseHP(_damageByThrownSalt);
            }
            
        }

    }

    // RUTINA PARA CONTROLAR EL TIEMPO DE ESPERA TRAS RECIBIR DAÑO
    private IEnumerator getDamageRoutine()
    {
        float delay = 1f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        while (true)
        {
            getDamage();
            yield return wait;
           
        }
    }

    // METODO PARA GESTIONAR LA PERDIDA DE VIDA (SAL)
    private void getDamage()
    {
        if (currentSaltAmount > 0)
        {

            currentSaltAmount -= _amountSaltLostByDamage;
            saltPouch.SetSaltAmount(currentSaltAmount);

            _enemyHealthRef.LoseHP(_damageByDamage);  // Si el enemigo nos daña, tambiEn resulta dañado por el contacto con la sal.

        }
        else
        {
            Debug.Log("Has muerto");
        }
    }

    /*-----------------------------------------------------------------------------------------------------------------------------*/

}

/*-----------------------------------------------------------------------------------------------------------------------------*/
