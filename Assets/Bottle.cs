using UnityEngine;

public class Bottle : MonoBehaviour, IDraggable
{
    public SpriteRenderer sprite;

    // Runtime
    string previousLayer;

    void Start()
    {
        GetComponent<Draggable>().behaviour.Result = this;
    }

    public void StartDrag()
    {
        previousLayer = sprite.sortingLayerName;
        sprite.sortingLayerName = Draggable.HoldingLayer;
    }

    public void Drop(DropReceiver obj)
    {
        sprite.sortingLayerName = previousLayer;
    }
}
