using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnterManor : MonoBehaviour
{
    [SerializeField] private GameObject _rainObject;

    private bool _playerInsideManor = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Has chocado con {other.tag}");
        if (other.CompareTag("Player"))
        {
            _playerInsideManor = !_playerInsideManor;
            // Apagamo la generación de partículas

            _rainObject.SetActive(!_playerInsideManor);
        }
    }   
}
