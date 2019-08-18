using UnityEngine;

public class IngredientDrag : MonoBehaviour
{
    bool dragging;
    Vector3 pressPosition;
    Vector3 offset;


    void Start()
    {
    }

    void OnMouseDrag()
    { 
        if (!dragging)
        { 
            pressPosition = MousePosition;
            offset = transform.position - pressPosition;
            dragging = true;
        }

        transform.position = MousePosition;// + offset;
    }

    Vector3 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition).WithZ(0);

    void OnMouseUp()
    {
        dragging = false;
    }
}