using System;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    // Serialized variables
    public IDraggableContainer behaviour;

    public bool CanDropOn(GameObject obj)
    {
        return behaviour.Result.CanDropOn(obj);
    }
}

public interface IDraggable
{
    bool CanDropOn(GameObject obj);
}

[Serializable]
public class IDraggableContainer : IUnifiedContainer<IDraggable> { }