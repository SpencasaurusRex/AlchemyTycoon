using UnityEngine;

public class MouseItem : MonoBehaviour
{
    
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition).WithZ(0);
    }
}
