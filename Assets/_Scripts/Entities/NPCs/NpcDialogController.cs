/* SCRIPT TO CONTROL NPC'S DIALOG BEHAVIOUR */
/*-----------------------------------------------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NpcDialogController : MonoBehaviour
{

/*-----------------------------------------------------------------------------------------------------------------------------*/

    /* VARIABLES */

    // EDTIABLE VALUES
    [SerializeField] private string NPC_message;
    
    // REFERENCES
    [SerializeField] private GameObject dialog_box_gameobject;  // UI Dialog box GameObject reference

/*-----------------------------------------------------------------------------------------------------------------------------*/

    /* BASIC METHODS */

    // START ACTION
    // Start action is called
    // Start is called before the first frame update
    void Start()
    {
        dialog_box_gameobject.SetActive(false);  // Disable Dialog Box
    }

/*-----------------------------------------------------------------------------------------------------------------------------*/

    /* TRIGGER METHODS */

    // ON TRIGGER ENTER ACTION
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))  // If the player enters the trigger
        {
            dialog_box_gameobject.GetComponentInChildren<TextMeshProUGUI>().text = NPC_message;  // Set NPC message in dialog box
            dialog_box_gameobject.SetActive(true);  // Enable Dialog Box
            GetComponents<AudioSource>()[0].Play();  // Play Dialog Sound
        }

    }

    // ON TRIGGER ENTER ACTION
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // If the player exits the trigger
        {
            dialog_box_gameobject.SetActive(false);  // Disable Dialog Box
            GetComponents<AudioSource>()[1].Play();  // Play Dialog Sound
        }
    }

/*-----------------------------------------------------------------------------------------------------------------------------*/

}

/*-----------------------------------------------------------------------------------------------------------------------------*/
