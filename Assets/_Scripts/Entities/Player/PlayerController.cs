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

    // EDITABLE PLAYER VALUES
    [Header("Player Stats")]
    private int maxSaltAmount = 100;
    private int currentSaltAmount;
    public SaltPouch saltPouch;
    [SerializeField] private int _saltThrowDistance;
    [SerializeField] private int _amountSaltLostByDamage;

    // Player's movement speed
    [Header("Player Movement Speed")]
    [SerializeField] [Range(1, 20)] private float speed = 2.5f;

    // Cursor mouse sensitivity
    [Header("Mouse sensitivity")]
    [SerializeField] [Range(1, 100)] private float mouseSensivity = 50;

    // REFERENCES

    // Reference to player's camera
    [SerializeField] private Camera playerCamera;

    // Reference to character controller
    private CharacterController characterController;

    // Reference to player's animator
    private Animator animator;

    // Reference to player's previous position
    private Vector3 playerPreviousPosition;

    #region Player Input
    // Reference to player's input system
    private PlayerInput playerInput;

    // Reference to player's input system actions
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

    // AUXILIAR VARIABLES

    // Variable to keep track of player's camera rotation in the x axis
    private float xRotation;

    private string selectableTag = "Selectable";
    private Transform _selection;
    private bool _canDamageCoroutine = true;

    // Referencias del enemigo 
    private GameObject _enemyRef;
    private EnemyHealthSystem _enemyHealthRef;
    [SerializeField] private int _damageRange;//Distance the enemy can damage the player
    [SerializeField] private int _damageByThrownSalt;//Damage the enemy takes by the thrown salt
    [SerializeField] private int _damageByDamage;//Damage the enemy takes by dealing damage

    /*-----------------------------------------------------------------------------------------------------------------------------*/

    /* BASIC METHODS */

    // AWAKE EVENT
    private void Awake()
    {

        // INPUT REFERENCES CREATION
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        grabAction = playerInput.actions["Grab"];
        inventoryAction = playerInput.actions["P_Inventory"];
        throwAction = playerInput.actions["Fire"];

        // Get Animator
        animator = GetComponent<Animator>();

        // Character controller reference creation
        characterController = GetComponent<CharacterController>();
        if (characterController==null) Debug.Log("CHARACTER CONTROLLER ES NULL");

    }

    // START ACTION
    // Start is called before the first frame update
    private void Start()
    {
        // SET REFERENCES' VALUES
        playerPreviousPosition = transform.position;  // Set previous position as the current position in the start
        currentSaltAmount = maxSaltAmount;
        saltPouch.SetMaxSaltAmount(maxSaltAmount);
        _enemyRef = GameObject.FindGameObjectWithTag("Enemy");
        _enemyHealthRef = _enemyRef.GetComponent<EnemyHealthSystem>();

    }

    // UPDATE ACTION
    // Update is called once per frame
    private void Update()
    {
 
        Move();  // Control player's movement
        Look();  // Control camera's movement
        
        if(Vector3.Distance(transform.position, _enemyRef.transform.position) < _damageRange && _canDamageCoroutine)
        {
            _canDamageCoroutine = false;
            StartCoroutine(getDamageRoutine());
        }
        else
        {
            if(!(Vector3.Distance(transform.position, _enemyRef.transform.position) < _damageRange))
            {
                StopCoroutine(getDamageRoutine());
                _canDamageCoroutine = true;
            }
            
        }

    } 

/*-----------------------------------------------------------------------------------------------------------------------------*/

    /* ENABLE AND DISABLE SCRIPTS METHODS */

    // ONENABLE EVENT
    // Calls when this script gets enabled
    private void OnEnable()
    {
        playerInput.enabled = true;
        grabAction.performed += Grab;
        inventoryAction.performed += Inventory;
        throwAction.performed += ThrowSalt;
    }

    // ONDISABLE EVENT
    // Calls when this script gets disabled
    private void OnDisable()
    {
        playerInput.enabled = false;
        grabAction.performed -= Grab;
        inventoryAction.performed -= Inventory;
        throwAction.performed -= ThrowSalt;
    }

/*-----------------------------------------------------------------------------------------------------------------------------*/

    private void CheckObject() {

        
        if (_selection != null) 
        {
            _selection = null;
        }
        

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


            if (selection.CompareTag(selectableTag))
            {            
                if (selection.TryGetComponent<ItemObject>(out ItemObject item)) 
                {
                    item.OnHandlePickupItem();
                }

                _selection = selection;
            }
        }
    }



/*-----------------------------------------------------------------------------------------------------------------------------*/

    /* MOVEMENT METHODS */

    // ACTION TO CONTROL PLAYER'S MOVEMENT
    private void Move()
    {

        // Movement vector creation
        Vector3 moveVector = transform.right * moveAction.ReadValue<Vector2>().x + transform.forward * moveAction.ReadValue<Vector2>().y;

        // Character controller movement controlled by the speed value
        characterController.Move(moveVector * Time.deltaTime * speed);

        //---------------------------------------------------------------------------------//
        // Walking animation controller

        // If player's current position is not different from previous position establish that the player is not walking
        if (Vector3.Distance(playerPreviousPosition, gameObject.transform.position) <= 0.05f) {
            animator.SetBool("is_walking", false);
        } else {
            // If it is different establish that the player is walking 
            animator.SetBool("is_walking", true);
        }
            
        //---------------------------------------------------------------------------------//

        // Set the current position as the previous one for the next iteration
        playerPreviousPosition = gameObject.transform.position;


    }

    // ACTION TO CONTROL CAMERA'S MOVEMENT
    private void Look()
    {

        // Get mouse delta values
        float mouseX = lookAction.ReadValue<Vector2>().x * mouseSensivity * Time.deltaTime;
        float mouseY = lookAction.ReadValue<Vector2>().y * mouseSensivity * Time.deltaTime;

        // Establish x rotation between -90 and 90 degrees
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        // Rotate camera in the y axis
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // Rotate whole player in the x axis
        this.transform.Rotate(Vector3.up * mouseX);

    }

    // ACTION TO TAKE/GRAB OBJECTS
    public void Grab(InputAction.CallbackContext context)
    {
        if (context.started && !animator.GetBool("can_grab")) 
        {
            // CHECK IF AN OBJECT IS GRABABLE(?)
            // ANIMATION PLAYS
            animator.SetBool("can_grab", true);
            CheckObject();
            animator.SetBool("can_grab", false);


            // OBJECT GOES TO INVENTORY (OTHER SCRIPT?)
            // OBJECT GRABBED GETS DESTROYED ON SCENE BUT STORED ON INVENTORY ARRAY
        }
    }

    // ACTION OPEN/CLOSE INVENTORY
    public void Inventory(InputAction.CallbackContext context)
    {
        bool performed = false;

        if (context.started && !performed) 
        {

            if (GameManager.Instance.State == GameState.Game)
                GameManager.Instance.ChangeState(GameState.Inventory);
            else if (GameManager.Instance.State == GameState.Inventory)
                GameManager.Instance.ChangeState(GameState.Game);

            performed = true;
        }

        performed = false;
    }

    // ACTION THROW SALT
    public void ThrowSalt(InputAction.CallbackContext context)
    {
        Debug.Log("Salt throw!");
        var saltAmount = 10;

        if (currentSaltAmount > 0) {
            currentSaltAmount -= saltAmount;
            saltPouch.SetSaltAmount(currentSaltAmount);
            if (Vector3.Distance(transform.position, _enemyRef.transform.position) < _saltThrowDistance)
            {
                _enemyHealthRef.LoseHP(_damageByThrownSalt);
            }
            
        }
    }

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

    private void getDamage()
    {
        if (currentSaltAmount > 0)
        {
            currentSaltAmount -= _amountSaltLostByDamage;
            saltPouch.SetSaltAmount(currentSaltAmount);
            _enemyHealthRef.LoseHP(_damageByDamage);
        }
        else
        {
            Debug.Log("Has muerto");
        }
    }

    /*-----------------------------------------------------------------------------------------------------------------------------*/

}

/*-----------------------------------------------------------------------------------------------------------------------------*/
