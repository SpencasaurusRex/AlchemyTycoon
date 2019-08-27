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
        Sharpness = 0;
    }
}