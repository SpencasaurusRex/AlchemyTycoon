using System;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public static string HoldingLayer = "Holding";
    public IDraggableContainer behaviour;

    public delegate void Reordered();
    public static event Reordered OnReordered;

    // Runtime
    SpriteRenderer sr;
    string previousLayer;

    void Awake()
    {
        OnReordered += Reorder;
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void StartDrag()
    {
        transform.SetAsLastSibling();
        previousLayer = sr.sortingLayerName;
        sr.sortingLayerName = HoldingLayer;

        OnReordered?.Invoke();
        behaviour.Result?.StartDrag();
    }

    public void Drop(DropReceiver obj)
    {
        sr.sortingLayerName = previousLayer;
        behaviour.Result?.Drop(obj);
    }

    void Reorder()
    {
        behaviour.Result?.Reorder(sr.sortingOrder = transform.GetSiblingIndex());
    }
}

public interface IDraggable
{
    void StartDrag();
    void Drop(DropReceiver obj);
    void Reorder(int siblingIndex);
}

[Serializable]
public class IDraggableContainer : IUnifiedContainer<IDraggable> { }