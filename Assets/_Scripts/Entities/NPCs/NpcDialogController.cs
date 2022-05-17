using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NpcDialogController : MonoBehaviour
{

    [SerializeField] private GameObject dialog_box_gameobject;

    void Start()
    {
        dialog_box_gameobject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialog_box_gameobject.GetComponentInChildren<TextMeshProUGUI>().text = "Test Text";
            dialog_box_gameobject.SetActive(true);
            GetComponents<AudioSource>()[0].Play();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialog_box_gameobject.SetActive(false);
            GetComponents<AudioSource>()[1].Play();
        }
    }

}
