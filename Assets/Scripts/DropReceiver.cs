using System;
using UnityEngine;

public class DropReceiver : MonoBehaviour
{
    // Serialized variables
    public IDropReceiverContainer behaviour;

    public bool Receive(GameObject obj)
    {
        return behaviour.Result.Receive(obj);
    }
}

public interface IDropReceiver
{
    bool Receive(GameObject obj);
}

[Serializable]
public class IDropReceiverContainer : IUnifiedContainer<IDropReceiver> { }