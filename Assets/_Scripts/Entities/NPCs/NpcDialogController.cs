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
    [SerializeField] private string _npcMessage;
    
    // REFERENCES
    [SerializeField] private GameObject _dialogBoxGameobject;  // UI Dialog box GameObject reference

/*-----------------------------------------------------------------------------------------------------------------------------*/

    /* BASIC METHODS */

    // START ACTION
    // Start action is called
    // Start is called before the first frame update
    void Start()
    {
        _dialogBoxGameobject.SetActive(false);  // Disable Dialog Box
    }

/*-----------------------------------------------------------------------------------------------------------------------------*/

    /* TRIGGER METHODS */

    // ON TRIGGER ENTER ACTION
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))  // If the player enters the trigger
        {
            _dialogBoxGameobject.GetComponentInChildren<TextMeshProUGUI>().text = _npcMessage;  // Set NPC message in dialog box
            _dialogBoxGameobject.SetActive(true);  // Enable Dialog Box
            GetComponents<AudioSource>()[0].Play();  // Play Dialog Sound
        }

    }

    // ON TRIGGER ENTER ACTION
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // If the player exits the trigger
        {
            _dialogBoxGameobject.SetActive(false);  // Disable Dialog Box
            GetComponents<AudioSource>()[1].Play();  // Play Dialog Sound
        }
    }

/*-----------------------------------------------------------------------------------------------------------------------------*/

}

/*-----------------------------------------------------------------------------------------------------------------------------*/
