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
    public List<Tag> Clickables;
    public int MinimumDragDistance = 10;

    // Runtime variables
    bool mousePressed;
    float timeSinceMousePress;
    Vector2 clickPositionPixels;
    int currentState;
    GameObject dragTarget;

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
            if (currentState == CLICK)
            {
                PerformClick();
            }
            else if (currentState == DRAG)
            {
                FinishDrag();
            }

            mousePressed = false;
            timeSinceMousePress = 0;
            currentState = CLICK;
        }
    }

    void StartDrag()
    {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(clickPositionPixels);
        print("Casting at " + clickPosition);
        if (Cast(clickPosition, Draggables, out var info))
        {
            print("Dragging " + info.collider.gameObject.name);
            dragTarget = info.collider.gameObject;
            currentState = DRAG;
        }
        else
        {
            print("Misclick");
            currentState = MISCLICK;
        }
    }

    void PerformDrag()
    {
        dragTarget.transform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 1);
    }

    void PerformClick()
    {
        print("Click");
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Cast(clickPosition, Clickables, out var info))
        {

        }
        else
        {

        }
    }

    void FinishDrag()
    {
        dragTarget.transform.position = dragTarget.transform.localPosition.WithZ(0);
    }

    bool Cast(Vector3 position, List<Tag> targets, out RaycastHit2D hitInfo)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector2.zero);
        hits = hits.OrderBy(x => x.distance).ToArray();

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.GetComponent<TagComponent>().HasAny(targets))
            {
                hitInfo = hit;
                return true;
            }
        }
        hitInfo = new RaycastHit2D();
        return false;
    }
}
