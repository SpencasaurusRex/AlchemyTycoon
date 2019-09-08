using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition).WithZ(0);
    }
}
