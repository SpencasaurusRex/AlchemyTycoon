using System;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    // Serialized variables
    public IClickableContainer behaviour;

    public bool Click()
    {
        return behaviour.Result?.Click() ?? false;
    }
}

public interface IClickable
{
    bool Click();
}

[Serializable]
public class IClickableContainer : IUnifiedContainer<IClickable> { }