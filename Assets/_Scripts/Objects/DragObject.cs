using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public static bool MouseButtonReleased;
    private Vector2 mousePosition;
    private float offsetX, offsetY;

    void OnMouseDown()
    {
        MouseButtonReleased  = false;
        offsetX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        offsetY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
    }

    void OnMouseDrag()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x - offsetX, mousePosition.y - offsetY);
    }
    void OnMouseUp()
    {
        MouseButtonReleased = true;
    }

    private void OnTriggerStay2D(Collider2D other) {

        string thisName = gameObject.name;
        string collisionName = other.gameObject.name;

        Debug.Log("THIS NAME: "+thisName);
        Debug.Log("THIS COLLISION NAME: "+collisionName);

        if (MouseButtonReleased) {
            if (thisName == "Dinning Room Key" && collisionName == "Salt") { // Receta 1
                Instantiate(Resources.Load("Salted_Key"), transform.position, Quaternion.identity);
                MouseButtonReleased = false;
                Destroy(other.gameObject);
                Destroy(gameObject);
            } else if (thisName == "Ingrediendte 1" && collisionName == "Ingrediente 2") { // Plantilla receta

            }
        }
        
        
    }
}
