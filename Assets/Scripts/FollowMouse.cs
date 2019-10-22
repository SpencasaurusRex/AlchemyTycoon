using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition).WithZ(0);
    }
}
