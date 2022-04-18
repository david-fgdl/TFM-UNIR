using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NpcDialogController : MonoBehaviour
{

    [SerializeField] private GameObject dialog_text_gameobject;

    void Start()
    {
        GetComponent<Image>().enabled = false;
        dialog_text_gameobject.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Image>().enabled = true;
            dialog_text_gameobject.GetComponent<TextMeshProUGUI>().enabled = true;
            GetComponents<AudioSource>()[0].Play();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Image>().enabled = false;
            dialog_text_gameobject.GetComponent<TextMeshProUGUI>().enabled = false;
            GetComponents<AudioSource>()[1].Play();
        }
    }

}
