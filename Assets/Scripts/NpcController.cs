/* SCRIPT TO CONTROL NPC'S BEHAVIOUR */
/*-----------------------------------------------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{

    /* VARIABLES */

    // REFERENCES

    [SerializeField] private GameObject player;  // Player's gameobject reference

/*-----------------------------------------------------------------------------------------------------------------------------*/

    /*BASIC METHODS */

    // UPDATE ACTION
    // Update is called once per frame
    void Update()
    {

        // NPC is constantly looking at the player (in the y axis only) - REFERENCE: https://www.youtube.com/watch?v=dp3lZUDij6Y
        transform.LookAt(new Vector3 (player.transform.position.x, transform.position.y, player.transform.position.z));

    }

/*-----------------------------------------------------------------------------------------------------------------------------*/

}

/*-----------------------------------------------------------------------------------------------------------------------------*/
