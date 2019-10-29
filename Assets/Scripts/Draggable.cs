using System;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public static string HoldingLayer = "Holding";
    public IDraggableContainer behaviour;

    // Runtime
    SpriteRenderer sr;
    string previousLayer;

    public void Drop(DropReceiver obj)
    {
        sr.sortingLayerName = previousLayer;
        behaviour.Result?.Drop(obj);
    }

    public void StartDrag()
    {
        previousLayer = sr.sortingLayerName;
        sr.sortingLayerName = HoldingLayer;
        behaviour.Result?.StartDrag();
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
}

public interface IDraggable
{
    void StartDrag();
    void Drop(DropReceiver obj);
}

[Serializable]
public class IDraggableContainer : IUnifiedContainer<IDraggable> { }