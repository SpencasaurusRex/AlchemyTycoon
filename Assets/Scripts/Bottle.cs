using UnityEngine;

public class Bottle : MonoBehaviour, IDraggable
{
    SpriteRenderer sprite;
    public SpriteRenderer childSprite;
    public string Liquid;

    // Runtime
    string previousLayer;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        GetComponent<Draggable>().behaviour.Result = this;
    }

    public void StartDrag()
    {
        previousLayer = childSprite.sortingLayerName;
        childSprite.sortingLayerName = Draggable.HoldingLayer;
    }

    public void Drop(DropReceiver obj)
    {
        childSprite.sortingLayerName = previousLayer;
    }

    public void Reorder(int index)
    {
        sprite.sortingOrder = index * 2;
        childSprite.sortingOrder = index * 2 - 1;
    }
}
