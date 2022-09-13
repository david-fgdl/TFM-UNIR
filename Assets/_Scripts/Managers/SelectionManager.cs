/* SCRIPT PARA GESTIONAR LAS SELECCIONES DE OBJETOS */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class SelectionManager : MonoBehaviour
{

    /* VARIABLES */

    // REFERENCIAS
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Shader _highlightShader;
    [SerializeField] private Shader _normalShader;

    // VARIABLES AUXILIARES
    private string _selectableTag = "Selectable";
    private Renderer[] _oldRenderers = Array.Empty<Renderer>();
    private Transform _selection;

    /* METODOS BASICOS */

    // METODO UPDATE
    // Update es llamado una vez por cada frame
    private void Update()
    {

        // SE QUITA EL SHADER POR DEFECTO
        if (_selection!=null)
        {
            var selectionRenderers = _selection.GetComponentsInChildren<Renderer>();
            if (selectionRenderers != null) 
            {
                foreach (var renderer in selectionRenderers)
                {
                    renderer.material.shader = _normalShader;
                }
                    
            } 
            _selection = null;
        }

        // SE CREA EL RAYCAST
        var ray = _playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        // SI EL OBJETO ESTA EN EL RANGO DEL RAYCAST SE APLICA EL SHADER
        if (Physics.Raycast(ray, out hit, 10f)) 
        {
            var selection = hit.transform;
            
            if (selection.CompareTag(_selectableTag))
            {
                // Esto es para cambiar el material y resaltar objetos
                var selectionRenderers = selection.GetComponentsInChildren<Renderer>();
                _oldRenderers = selectionRenderers;
                if (selectionRenderers != null) {
                    foreach (var renderer in selectionRenderers)
                        renderer.material.shader = _highlightShader;
                }
                _selection = selection;
            }   

        }
    }
}
