using System;
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
    
    // Configuration
    public int MinimumDragDistance = 10;

    // Runtime
    bool mousePressed;
    float timeSinceMousePress;
    Vector2 clickPositionPixels;
    int currentState;
    GameObject dragTarget;
    Draggable dragComp;

    Vector3 MousePositionPixels => Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
        if (Cast(MousePositionPixels, typeof(Draggable), out var info))
        {
            dragTarget = info.collider.gameObject;
            dragComp = dragTarget.GetComponent<Draggable>();
            dragComp.StartDrag();
            currentState = DRAG;
        }
        else
        {
            currentState = MISCLICK;
        }
    }

    void PerformDrag()
    {
        dragTarget.transform.localPosition = MousePositionPixels.WithZ(0);
    }

    void PerformClick()
    {
        if (Cast(MousePositionPixels, typeof(Clickable), out var info))
        {
            info.collider.gameObject.GetComponent<Clickable>().Click();
        }
        else
        {

        }
    }

    void FinishDrag()
    {
        if (Cast(MousePositionPixels, typeof(DropReceiver), out var info))
        {
            var receiver = info.collider.gameObject.GetComponent<DropReceiver>();
            if (receiver.Receive(dragTarget))
            {
                dragComp.Drop(receiver);
            }
            else dragComp.Drop(null);
        }
        else dragComp.Drop(null);

        dragTarget.transform.position = dragTarget.transform.localPosition;
        dragTarget = null;
        dragComp = null;
    }

    bool Cast(Vector3 position, Type component, out RaycastHit2D hitInfo)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector2.zero);
        // Oof
        hits = hits.OrderByDescending(x => SortingLayer.GetLayerValueFromID(x.collider.GetComponent<SpriteRenderer>().sortingLayerID)).ThenByDescending(x => x.collider.GetComponent<SpriteRenderer>().sortingOrder).ToArray();

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.GetComponent(component) != null)
            {
                hitInfo = hit;
                return true;
            }
        }
        hitInfo = new RaycastHit2D();
        return false;
    }
}
