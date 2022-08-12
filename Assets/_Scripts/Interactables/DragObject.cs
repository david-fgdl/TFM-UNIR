using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IInitializePotentialDragHandler, IDropHandler
{
    public static bool MouseButtonReleased;
    private Vector2 _mousePosition;
    private float _offsetX, _offsetY;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("CLICKANDO OBJETO");
        MouseButtonReleased  = false;
        _offsetX = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x - transform.position.x;
        _offsetY = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).y - transform.position.y;
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = new Vector2(_mousePosition.x - _offsetX, _mousePosition.y - _offsetY);
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MouseButtonReleased = true;
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        string thisName = gameObject.name;
        string collisionName = other.gameObject.name;

        Debug.Log("THIS NAME: "+thisName);
        Debug.Log("THIS COLLISION NAME: "+collisionName);

        if (MouseButtonReleased) 
        {
            if (thisName == "Dinning Room Key" && collisionName == "Salt") // Receta 1
            {
                Instantiate(Resources.Load("Salted_Key"), transform.position, Quaternion.identity);
                MouseButtonReleased = false;
                Destroy(other.gameObject);
                Destroy(gameObject);
            } 
        }
    }

    
}
