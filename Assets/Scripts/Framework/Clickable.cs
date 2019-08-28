using UnityEngine;

public class Clickable : MonoBehaviour
{
    void OnMouseDown()
    {
        print("Mouse Down " + name);
        GameManager.Instance.MouseDown(this);
    }

    void OnMouseUp()
    {
        print("Mouse Up " + name);
        GameManager.Instance.MouseUp(this);
    }

    void OnMouseDrag()
    {
        print("Mouse Drag " + name);
        GameManager.Instance.MouseDrag(this);
    }
}