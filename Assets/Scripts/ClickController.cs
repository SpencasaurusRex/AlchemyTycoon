using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    // Const variables
    const int LEFT = 0;
    const int RIGHT = 1;
    const int MIDDLE = 2;

    const int CLICK = 0;
    const int DRAG = 1;
    const int MISCLICK = 2;

    // Singleton reference
    public static ClickController Instance; 
    
    // Serializable variables
    public List<Tag> Draggables;
    public List<Tag> Droppables;
    public int MinimumDragDistance = 10;

    // Runtime variables
    bool mousePressed;
    float timeSinceMousePress;
    Vector2 clickPositionPixels;
    int currentState;

    void Start()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        // First clicked
        if (Input.GetMouseButtonDown(LEFT))
        {
            mousePressed = true;
            timeSinceMousePress = 0;
            clickPositionPixels = Input.mousePosition;
        }
        // Hold click
        else if (mousePressed && Input.GetMouseButton(LEFT))
        {
            timeSinceMousePress += Time.deltaTime;
            if (currentState == CLICK)
            {
                // Check to see how far we have dragged
                Vector2 distance = (Vector2)Input.mousePosition - clickPositionPixels;
                if (distance.magnitude >= MinimumDragDistance)
                {
                    StartDrag();
                }
            }
            if (currentState == DRAG)
            {
                PerformDrag();
            }
        }
        // Stopped clicking
        else if (mousePressed && !Input.GetMouseButton(LEFT))
        {
            mousePressed = false;
            timeSinceMousePress = 0;

            if (currentState == CLICK)
            {
                PerformClick();
            }
            else if (currentState == DRAG)
            {
                
            }
        }
    }

    void StartDrag()
    {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(clickPositionPixels);
        print("Casting at " + clickPosition);
        if (Cast(clickPosition, Draggables, out var info))
        {
            print("Dragging " + info.collider.gameObject.name);
            currentState = DRAG;
        }
        print("Misclick");
        currentState = MISCLICK;
    }

    void PerformDrag()
    {
        
    }

    void PerformClick()
    {
        print("Click");
    }

    bool Cast(Vector3 position, List<Tag> targets, out RaycastHit hitInfo)
    {
        Ray ray = new Ray(position, Vector3.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, 100);
        
        hits = hits.OrderBy(x => x.distance).ToArray();

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.GetComponent<TagComponent>().HasAny(targets))
            {
                hitInfo = hit;
                return true;
            }
        }
        hitInfo = new RaycastHit();
        return false;
    }
}
