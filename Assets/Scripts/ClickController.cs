using System;
using System.Linq;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    // Const variables
    const int LEFT = 0;
    const int RIGHT = 1;
    const int MIDDLE = 2;

    const int MISCLICK = -1;
    const int NONE = 0;
    const int CLICK = 1;
    const int DRAG = 2;

    // Singleton reference
    public static ClickController Instance; 
    
    // Configuration
    public int MinimumDragDistance = 3;

    // Runtime
    float timeSinceMousePress;
    Vector2 clickPositionPixels;
    int currentState;
    Interactable target;

    Vector3 MousePositionWorld => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    public delegate void MisclickDown(Vector3 pos);
    public static event MisclickDown OnMisclickDown;
    public delegate void MisclickRelease(Vector3 pos);
    public static event MisclickRelease OnMisclickRelease;

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
        if (currentState < 1 && Input.GetMouseButtonDown(LEFT))
        {
            timeSinceMousePress = 0;
            clickPositionPixels = Input.mousePosition;

            if (Cast(x => x.Clickable || x.Draggable, out target))
            {
                if (target.Clickable) target.InvokeClickDown();
                currentState = CLICK;
            }
            else
            {
                OnMisclickDown?.Invoke(MousePositionWorld);
                currentState = MISCLICK;
            }
        }
        // Hold click
        else if (currentState > NONE && Input.GetMouseButton(LEFT))
        {
            timeSinceMousePress += Time.deltaTime;
            if (currentState == CLICK)
            {
                // Check to see how far we have dragged
                Vector2 distance = (Vector2)Input.mousePosition - clickPositionPixels;
                if (target.Draggable && distance.magnitude >= MinimumDragDistance)
                {
                    StartDrag();
                }
                else if (target.Clickable)
                {
                    target.InvokeClickHold();
                }
            }
            if (currentState == DRAG)
            {
                PerformDrag();
            }
        }
        // Stopped clicking
        else if (currentState != NONE && !Input.GetMouseButton(LEFT))
        {
            if (currentState == CLICK && target.Clickable)
            {
                ReleaseClick();
            }
            else if (currentState == DRAG)
            {
                ReleaseDrag();
            }
            else if (currentState == MISCLICK)
            {
                ReleaseMisclick();
            }

            timeSinceMousePress = 0;
            currentState = NONE;
            target = null;
        }
    }

    void StartDrag()
    {
        currentState = DRAG;
        target.InvokeStartDrag();
    }

    void PerformDrag()
    {
        target.transform.position = MousePositionWorld.WithZ(0);

        if (Cast(x => x.DropReceiver && x != target, out var receiver))
        {
            target.InvokeDrag(receiver);
        }
        else target.InvokeDrag(null);
    }

    void ReleaseDrag()
    {
        if (Cast(x => x.DropReceiver && x != target, out var receiver))
        {
            if (receiver.CanReceive(target.gameObject))
            {
                target.InvokeDrop(target.gameObject, receiver);
                receiver.InvokeReceive(target.gameObject);
            }
            else target.InvokeDrop(target.gameObject, receiver);
        }
        else target.InvokeDrop(target.gameObject, null);
    }

    void ReleaseClick()
    {
        target.InvokeClickRelease();
    }

    void ReleaseMisclick()
    {
        OnMisclickRelease?.Invoke(MousePositionWorld);
    }

    bool Cast(Predicate<Interactable> condition, out Interactable result)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(MousePositionWorld, Vector2.zero);

        Interactable[] interactables = hits.Select(x => x.collider.gameObject.GetComponent<Interactable>())
                .Where(x => x != null && condition(x))
                .OrderByDescending(x => x.Layer)
                .ThenByDescending(x => x.Order).ToArray();

        result = interactables.FirstOrDefault();

        return interactables.Length > 0;
    }
}
