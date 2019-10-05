using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneObject : MonoBehaviour
{
   [ShowInInspector]
    public Component[] KeepActive;

    public delegate void HideEvent();
    public event HideEvent OnHide;

    public delegate void ShowEvent();
    public event ShowEvent OnShow;

    Dictionary<Component, bool> PreviouslyActive = new Dictionary<Component, bool>();

    void Start()
    {
        GameController.Instance.Register(this);
    }

    public void Show()
    {
        OnShow?.Invoke();

        foreach (var behaviour in GetComponents<Behaviour>())
        {
            if (PreviouslyActive.TryGetValue(behaviour, out bool enabled) && enabled)
            {
                behaviour.enabled = true;
            }
        }
        foreach (var renderer in GetComponents<Renderer>())
        {
            if (PreviouslyActive.TryGetValue(renderer, out bool enabled) && enabled)
            {
                renderer.enabled = true;
            }
        }
        foreach (var collider in GetComponents<Collider>())
        {
            if (PreviouslyActive.TryGetValue(collider, out bool enabled) && enabled)
            {
                collider.enabled = true;
            }
        }

        PreviouslyActive.Clear();
    }

    public void Hide()
    {
        OnHide?.Invoke();

        foreach (var behaviour in GetComponents<Behaviour>())
        {
            if (behaviour == this) continue;
            if (KeepActive.Contains(behaviour)) continue;
            PreviouslyActive.Add(behaviour, behaviour.enabled);
            behaviour.enabled = false;
        }
        foreach(var renderer in GetComponents<Renderer>())
        {
            PreviouslyActive.Add(renderer, renderer.enabled);
            if (KeepActive.Contains(renderer)) continue;
            renderer.enabled = false;
        }
        foreach (var collider in GetComponents<Collider>())
        {
            PreviouslyActive.Add(collider, collider.enabled);
            if (KeepActive.Contains(collider)) continue;
            collider.enabled = true;
        }
    }

    public void OnDestroy()
    {
        GameController.Instance.Unregister(this);
    }
}