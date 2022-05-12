/* SCRIPT TO CONTROL PLAYER'S BEHAVIOUR */
/*-----------------------------------------------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

/*-----------------------------------------------------------------------------------------------------------------------------*/

    /* VARIABLES */

    // EDITABLE PLAYER VALUES
    [Header("Player Stats")]
    private int maxSaltAmount = 100;
    private int currentSaltAmount;

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

    [SerializeField] private string selectableTag = "Selectable";
    private Transform _selection;

    

/*-----------------------------------------------------------------------------------------------------------------------------*/

    /* BASIC METHODS */

    // AWAKE EVENT
    void Awake()
    {

        // INPUT REFERENCES CREATION
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        grabAction = playerInput.actions["Grab"];
        inventoryAction = playerInput.actions["P_Inventory"];

        // Character controller reference creation
        characterController = GetComponent<CharacterController>();
        if (characterController==null) Debug.Log("CHARACTER CONTROLLER ES NULL");

    }

    // START ACTION
    // Start is called before the first frame update
    void Start()
    {
        // SET REFERENCES' VALUES
        playerPreviousPosition = transform.position;  // Set previous position as the current position in the start
        currentSaltAmount = maxSaltAmount;
    }

    // UPDATE ACTION
    // Update is called once per frame
    void Update()
    {
 
        Move();  // Control player's movement
        Look();  // Control camera's movement
       
    }

/*-----------------------------------------------------------------------------------------------------------------------------*/

    /* ENABLE AND DISABLE SCRIPTS METHODS */

    // ONENABLE EVENT
    // Calls when this script gets enabled
    void OnEnable()
    {
        playerInput.enabled = true;
    }

    // ONDISABLE EVENT
    // Calls when this script gets disabled
    void OnDisable()
    {
        playerInput.enabled = false;
    }

/*-----------------------------------------------------------------------------------------------------------------------------*/

    void CheckObject() {
        if (_selection != null) {
            _selection = null;
        }

        var ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {

            
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                // Esto es para cambiar el material y resaltar objetos
                // var selectionRenderer = selection.GetComponent<Renderer>();
                // if (selectionRenderer != null) {
                //     selectionRenderer.material = highlightMaterial;
                // }

                if (selection.TryGetComponent<ItemObject>(out ItemObject item)) {
                    item.OnHandlePickupItem();
                }

                Debug.Log("golpisa de "+selection.name);

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
            GetComponent<Animator>().SetBool("is_walking", false);
        } else {
            // If it is different establish that the player is walking 
            GetComponent<Animator>().SetBool("is_walking", true);
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

        grabAction.performed +=
            _ =>
                {
                    // CHECK IF AN OBJECT IS GRABABLE(?)
                    // ANIMATION PLAYS
                    if (!GetComponent<Animator>().GetBool("can_grab")) {
                        GetComponent<Animator>().SetBool("can_grab", true);
                        CheckObject();

                    }
                    else GetComponent<Animator>().SetBool("can_grab", false);
                        
                    // OBJECT GOES TO INVENTORY (OTHER SCRIPT?)
                    // OBJECT GRABBED GETS DESTROYED ON SCENE BUT STORED ON INVENTORY ARRAY
                };
    }

    // ACTION OPEN/CLOSE INVENTORY
    public void Inventory(InputAction.CallbackContext context)
    {
        inventoryAction.performed +=
            _ =>
                {
                    if (GameManager.Instance.State == GameState.Game) {
                        // Inventory closed
                        Debug.Log("OPENING INVENTORY");
                        GameManager.Instance.ChangeState(GameState.Inventory);
                    } else if (GameManager.Instance.State == GameState.Inventory) {
                        // Inventory open
                        Debug.Log("CLOSING INVENTORY");
                        GameManager.Instance.ChangeState(GameState.Game);
                    } 
                };
    }

/*-----------------------------------------------------------------------------------------------------------------------------*/

}

/*-----------------------------------------------------------------------------------------------------------------------------*/
