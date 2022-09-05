/* SCRIPT PARA CONTROLAR LA ENTRADA EN LA MANSION */

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnterManor : MonoBehaviour
{

    /* VARIABLES */

    // REFERENCIAS
    [SerializeField] private GameObject _rainObject;

    // FLAGS
    private bool _playerInsideManor = false;

    /* METODOS DE TRIGGER */

    // METODO ON TRIGGER ENTER PARA QUE LA LLUVIA PARE TRAS ENTRAR EN LA MANSION
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Has chocado con {other.tag}");
        if (other.CompareTag("Player"))
        {
            _playerInsideManor = !_playerInsideManor;

            _rainObject.SetActive(!_playerInsideManor);
        }
    }

}
