using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    private string _selectableTag = "Selectable";
    [SerializeField] private Shader _highlightShader;
    private Transform _selection;



    // Update is called once per frame
    private void Update()
    {
        if (_selection!=null)
        {
            var selectionRenderers = _selection.GetComponentsInChildren<Renderer>();
            if (selectionRenderers != null) 
            {
                // foreach (var renderer in selectionRenderers)
                    // renderer.material.shader;
            } 
            _selection = null;
        }

        var ray = _playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10f)) 
        {
            var selection = hit.transform;
            
            if (selection.CompareTag(_selectableTag))
            {
                // Esto es para cambiar el material y resaltar objetos
                var selectionRenderers = selection.GetComponentsInChildren<Renderer>();
                if (selectionRenderers != null) {
                    foreach (var renderer in selectionRenderers)
                        renderer.material.shader = _highlightShader;
                }
                _selection = selection;
            }   

        }
    }
}
