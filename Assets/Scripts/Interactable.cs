using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Configuration
    public bool Clickable;
    public bool Draggable;
    public bool DropReceiver;

    // Runtime
    SpriteRenderer sr;
    List<IDragReceiver> DragReceivers = new List<IDragReceiver>();

    public int Layer => SortingLayer.GetLayerValueFromID(sr.sortingLayerID);
    public int Order => sr.sortingOrder;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public bool CanReceive(GameObject obj)
    {
        if (!DropReceiver) return false;
        foreach (var receiver in DragReceivers)
        {
            if (receiver.CanReceive(obj))
            {
                return true;
            }
        }
        return false;
    }

    public void Register(IDragReceiver receiver)
    {
        DragReceivers.Add(receiver);
    }
    
    public delegate void ClickDown();
    public event ClickDown OnClickDown;
    public void InvokeClickDown() => OnClickDown?.Invoke();

    public delegate void ClickHold();
    public event ClickHold OnClickHold;
    public void InvokeClickHold() => OnClickHold?.Invoke();

    public delegate void ClickRelease();
    public event ClickRelease OnClickRelease;
    public void InvokeClickRelease() => OnClickRelease?.Invoke();

    public delegate void StartDrag();
    public event StartDrag OnStartDrag;
    public void InvokeStartDrag() => OnStartDrag?.Invoke();

    public delegate void Drag(Interactable over);
    public event Drag OnDrag;
    public void InvokeDrag(Interactable over) => OnDrag?.Invoke(over);

    public delegate void Drop(Interactable on);
    public event Drop OnDrop;
    public void InvokeDrop(Interactable on) => OnDrop?.Invoke(on);

    public delegate void Receive(GameObject obj);
    public event Receive OnReceive;
    public void InvokeReceive(GameObject obj) => OnReceive?.Invoke(obj);
}