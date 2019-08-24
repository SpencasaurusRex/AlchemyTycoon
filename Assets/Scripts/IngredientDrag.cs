using UnityEngine;

public class IngredientDrag : MonoBehaviour
{
    public float Sharpness;
    public bool Dragging;

    void OnMouseDrag()
    { 
        Dragging = true;
    }

    void OnMouseUp()
    { 
        Dragging = false;
    }

    void Update()
    { 
        if (!Dragging) return;
        transform.position = Vector3.Lerp(transform.position, MousePosition, 1 - Mathf.Exp(-Sharpness * Time.deltaTime));
        Sharpness += Time.deltaTime * 10;
    }

    Vector3 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition).WithZ(0);
}