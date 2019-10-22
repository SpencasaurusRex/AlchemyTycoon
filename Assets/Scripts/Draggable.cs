using System;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    // Serialized variables
    public IDraggableContainer behaviour;

    public bool ValidDrop(GameObject obj)
    {
        return behaviour.Result.ValidDrop(obj);
    }
}

public interface IDraggable
{
    bool ValidDrop(GameObject obj);
}

[Serializable]
public class IDraggableContainer : IUnifiedContainer<IDraggable> { }