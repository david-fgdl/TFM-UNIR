/* SCRIPT PARA CONTROLAR EL COMPORTAMIENTO DEL JUGADOR */

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{

    /* VARIABLES */

    // STATS DEL JUGADOR
    [Header("Player Stats")]
    private int _maxSaltAmount = 100;
    private int _currentSaltAmount;
    public SaltPouch saltPouch;
    [SerializeField] private int _saltThrowDistance;
    [SerializeField] private int _amountSaltLostByDamage;

    // VELOCIDAD DE MOVIMIENTO DEL JUGADOR
    [Header("Player Movement Speed")]
    [SerializeField] [Range(1, 20)] private float _speed = 2.5f;

    // SENSIBILIDAD DEL CURSOR
    [Header("Mouse sensitivity")]
    [SerializeField] [Range(1, 100)] private float _mouseSensivity = 50;

    // REFERENCIAS

    [SerializeField] private Camera _playerCamera;

    private CharacterController _characterController;
    private Animator _animator;

    private Vector3 _playerPreviousPosition;

    #region Player Input

        private PlayerInput _playerInput;

        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _grabAction;
        private InputAction _inventoryAction;
        private InputAction _throwAction;

    #endregion

    #region Audio

        [Header("Audio Clips")]

        [SerializeField] private AudioClip _breathingSound;
        [SerializeField] private AudioClip _walkingSound;
        [SerializeField] private AudioClip _standardMusic;

    #endregion

    // ENEMIGO
    
    private GameObject _enemyRef;
    private EnemyHealthSystem _enemyHealthRef;
    [SerializeField] private int _damageRange;//Distance the enemy can damage the player
    [SerializeField] private int _damageByThrownSalt;//Damage the enemy takes by the thrown salt
    [SerializeField] private int _damageByDamage;//Damage the enemy takes by dealing damage

    // VARIABLES AUXILIARES

    private float _xRotation;  // Variable para conocer la posiciOn de la cAmara en el eje x

    private string _selectableTag = "Selectable";
    private Transform _selection;
    private bool _canDamageCoroutine = true;
    private Coroutine _routine;

    /* METODOS BASICOS */

    // METODO AWAKE
    private void Awake()
    {

        // ESTABLECER LAS REFERENCIAS

        _playerInput = GetComponent<PlayerInput>();

        _moveAction = _playerInput.actions["Move"];
        _lookAction = _playerInput.actions["Look"];
        _grabAction = _playerInput.actions["Grab"];
        _inventoryAction = _playerInput.actions["P_Inventory"];
        _throwAction = _playerInput.actions["Fire"];

        _animator = GetComponent<Animator>();

        _characterController = GetComponent<CharacterController>();
        if (_characterController==null) Debug.Log("CHARACTER CONTROLLER ES NULL");

    }

    // METODO START
    // Start es un mEtodo llamado antes del primer frame
    private void Start()
    {

        _playerPreviousPosition = transform.position;  // La posiciOn de comienzo se guarda como la posiciOn anterior

        // FIJAR VALORES DE SAL
        _currentSaltAmount = _maxSaltAmount;
        saltPouch.SetMaxSaltAmount(_maxSaltAmount);

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

        CheckEnemyDistance(transform.position, _enemyRef.transform.position);
        
    } 

    // METODO DE COMPROBACION DE DISTANCIA CON EL ENEMIGO
    // Si está más cerca del damageRange, puede dañar al jugaddor.
    private void CheckEnemyDistance(Vector3 playerPosition, Vector3 enemyPosition)
    {
        // GESTION DE LA INTERACCION CON EL ENEMIGO
        if (Vector3.Distance(playerPosition, enemyPosition) < _damageRange && _canDamageCoroutine)
        {
            _canDamageCoroutine = false;
            _routine = StartCoroutine(GetDamageRoutine());
            Debug.Log("Debo comenzar a recibir daño");
            
        }
        else
        {
            if (_routine == null) return;
            if(!(Vector3.Distance(playerPosition, enemyPosition) < _damageRange))
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
        _playerInput.enabled = true;
        _grabAction.performed += Grab;
        _inventoryAction.performed += Inventory;
        _throwAction.performed += ThrowSalt;
    }

    // METODO DE DESACTIVACION
    // Es llamado cuando se desactiva el script
    private void OnDisable()
    {
        _playerInput.enabled = false;
        _grabAction.performed -= Grab;
        _inventoryAction.performed -= Inventory;
        _throwAction.performed -= ThrowSalt;
    }

    /* METODOS DE MOVIMIENTO*/

    // METODO PARA CONTROLAR EL DESPLAZAMIENTO DEL JUGADOR
    private void Move()
    {

        Vector3 moveVector = transform.right * _moveAction.ReadValue<Vector2>().x + transform.forward * _moveAction.ReadValue<Vector2>().y;
        _characterController.Move(moveVector * Time.deltaTime * _speed);

        // GESTOR DE ANIMACIONES
        if (Vector3.Distance(_playerPreviousPosition, gameObject.transform.position) <= 0.05f)  _animator.SetBool("is_walking", false);
        else  _animator.SetBool("is_walking", true);

        // GUARDAR LA POSICION ACTUAL COMO LA POSICION ANTERIOR
        _playerPreviousPosition = gameObject.transform.position;


    }

    // METODO PARA CONTROLAR EL MOVIMIENTO DE LA CAMARA
    private void Look()
    {

        // OBTENER VALORES DELTA DEL RATON
        float mouseX = _lookAction.ReadValue<Vector2>().x * _mouseSensivity * Time.deltaTime;
        float mouseY = _lookAction.ReadValue<Vector2>().y * _mouseSensivity * Time.deltaTime;

        // ESTABLECER Y DELIMITAR LA ROTACION EN EL EJE X
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90);

        // ESTABLECER LA ROTACION EN EL EJE Y
        _playerCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);

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

        var ray = _playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
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
        if (context.started && !_animator.GetBool("can_grab")) 
        {

            // CHEQUEO Y GESTION DE ANIMACIONES
            _animator.SetBool("can_grab", true);
            CheckObject();
            _animator.SetBool("can_grab", false);

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

        if (_currentSaltAmount > 0) {

            // FIJAR CANTIDAD DE SAL
            _currentSaltAmount -= saltAmount;
            saltPouch.SetSaltAmount(_currentSaltAmount);

            // CONTROLAR DISPARO
            if (Vector3.Distance(transform.position, _enemyRef.transform.position) < _saltThrowDistance)
            {
                _enemyHealthRef.LoseHP(_damageByThrownSalt);
            }
            
        }

    }

    // RUTINA PARA CONTROLAR EL TIEMPO DE ESPERA TRAS RECIBIR DAÑO
    private IEnumerator GetDamageRoutine()
    {
        float delay = 1f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        while (true)
        {
            GetDamage();
            yield return wait;
           
        }
    }

    // METODO PARA GESTIONAR LA PERDIDA DE VIDA (SAL)
    private void GetDamage()
    {
        if (_currentSaltAmount > 0)
        {

            _currentSaltAmount -= _amountSaltLostByDamage;
            saltPouch.SetSaltAmount(_currentSaltAmount);

            _enemyHealthRef.LoseHP(_damageByDamage);  // Si el enemigo nos daña, tambiEn resulta dañado por el contacto con la sal.

        }
        else Debug.Log("Has muerto");
    }

    /*-----------------------------------------------------------------------------------------------------------------------------*/

}

/*-----------------------------------------------------------------------------------------------------------------------------*/
