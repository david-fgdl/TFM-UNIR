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

    // Player's movement speed
    [Header("Player Movement Speed")]
    [SerializeField] [Range(1, 20)] private float speed = 2.5f;

    // Cursor mouse sensitivity
    [Header("Mouse sensitivity")]
    [SerializeField] [Range(1, 100)] private float mouse_sensitivity = 50;

    // REFERENCES

    // Reference to player's camera
    [SerializeField] Camera player_camera;

    // Reference to character controller
    private CharacterController character_controller;

    // Reference to player's previous position
    private Vector3 player_previous_position;

    #region Player Input
    // Reference to player's input system
    private PlayerInput player_input;

    // Reference to player's input system actions
    private InputAction move_action;
    private InputAction look_action;
    private InputAction grab_action;
    private InputAction inventory_action;
    #endregion

    // AUXILIAR VARIABLES

    // Variable to keep track of player's camera rotation in the x axis
    private float x_rotation;

/*-----------------------------------------------------------------------------------------------------------------------------*/

    /* BASIC METHODS */

    // AWAKE EVENT
    void Awake()
    {
        // LOCKING CURSOR ON WINDOW
        Cursor.lockState = CursorLockMode.Locked;


        // SETTING PLAYER INPUT BEFORE ANYTHING ELSE
        if (GameManager.Instance.State == GameState.Game)
        {
            player_input = GetComponent<PlayerInput>();
            move_action = player_input.actions["Walk"];
            look_action = player_input.actions["Look"];
            grab_action = player_input.actions["Grab"];
            inventory_action = player_input.actions["Inventory"];
            character_controller = GetComponent<CharacterController>();
        }




    }

    // START ACTION
    // Start is called before the first frame update
    void Start()
    {
        // // CHANGE GAME STATE TO GAME TO SHOW UI
        // GameManager.Instance.ChangeState(GameState.Game); // CREO QUE NO HACE FALTA
        

        // SET REFERENCES' VALUES
        player_previous_position = transform.position;  // Set previous position as the current position in the start

    }

    // UPDATE ACTION
    // Update is called once per frame
    void Update()
    {
        Move();  // Control player's movement
        Look();  // Control camera's movement
    }

    // ONENABLE EVENT
    // Calls when this script gets enabled
    void OnEnable()
    {
        player_input.enabled = true;
    }

    // ONDISABLE EVENT
    // Calls when this script gets disabled
    void OnDisable()
    {
        player_input.enabled = false;
    }

/*-----------------------------------------------------------------------------------------------------------------------------*/

    /* MOVEMENT METHODS */

    // ACTION TO CONTROL PLAYER'S MOVEMENT
    private void Move()
    {

        // Movement vector creation
        Vector3 move_vector = transform.right * move_action.ReadValue<Vector2>().x + transform.forward * move_action.ReadValue<Vector2>().y;

        // Character controller movement controlled by the speed value
        character_controller.Move(move_vector * Time.deltaTime * speed);

        //---------------------------------------------------------------------------------//
        // Walking animation controller

        // If player's current position is not different from previous position establish that the player is not walking
        if (Vector3.Distance(player_previous_position, gameObject.transform.position) <= 0.05f) GetComponent<Animator>().SetBool("is_walking", false);
        else GetComponent<Animator>().SetBool("is_walking", true); // If it is different establish that the player is walking
        //---------------------------------------------------------------------------------//

        // Set the current position as the previous one for the next iteration
        player_previous_position = gameObject.transform.position;


    }

    // ACTION TO CONTROL CAMERA'S MOVEMENT
    private void Look()
    {

        // Get mouse delta values
        float mouse_x = look_action.ReadValue<Vector2>().x * mouse_sensitivity * Time.deltaTime;
        float mouse_y = look_action.ReadValue<Vector2>().y * mouse_sensitivity * Time.deltaTime;

        // Establish x rotation between -90 and 90 degrees
        x_rotation -= mouse_y;
        x_rotation = Mathf.Clamp(x_rotation, -90, 90);

        // Rotate camera in the y axis
        player_camera.transform.localRotation = Quaternion.Euler(x_rotation, 0, 0);

        // Rotate whole player in the x axis
        this.transform.Rotate(Vector3.up * mouse_x);

    }

    // ACTION TO TAKE/GRAB OBJECTS
    public void Grab(InputAction.CallbackContext context)
    {
        grab_action.performed +=
            context =>
                {
                    Debug.Log("GRABBING");
                    // CHECK IF AN OBJECT IS GRABABLE(?)
                    // ANIMATION PLAYS
                    if (!GetComponent<Animator>().GetBool("can_grab")) GetComponent<Animator>().SetBool("can_grab", true);
                    else GetComponent<Animator>().SetBool("can_grab", false);
                        
                    // OBJECT GOES TO INVENTORY (OTHER SCRIPT?)
                    // OBJECT GRABBED GETS DESTROYED ON SCENE BUT STORED ON INVENTORY ARRAY
                };
    }

    // ACTION OPEN/CLOSE INVENTORY
    public void Inventory(InputAction.CallbackContext context)
    {
        inventory_action.performed +=
            context =>
                {
                    if (GameManager.Instance.State == GameState.Inventory) {
                        // Inventory open
                        Debug.Log("CLOSING INVENTORY");
                        GameManager.Instance.ChangeState(GameState.Game);
                    } else {
                        // Inventory closed
                        Debug.Log("OPENING INVENTORY");
                        GameManager.Instance.ChangeState(GameState.Inventory);
                    }
                };
    }

/*-----------------------------------------------------------------------------------------------------------------------------*/

}

/*-----------------------------------------------------------------------------------------------------------------------------*/
