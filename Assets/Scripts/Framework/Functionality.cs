using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Functionality
{ 
    public Type[] SubscribedComponents;

    public Functionality(params Type[] components)
    { 
        if (components.Length == 0) throw new ArgumentException("Need to provide at least one subscribed component type");
        SubscribedComponents = components;
    }

    public T Cast<T>(UnityEngine.Object obj) where T : UnityEngine.Object
    {
        return (T)obj;
    }

    public Vector3 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition).WithZ(0);

    public virtual void Update(List<UnityEngine.Object> objects) { }
    public virtual void FixedUpdate(List<UnityEngine.Object> objects) { }
    public virtual void MouseDown(UnityEngine.Object obj) { }
    public virtual void MouseDrag(UnityEngine.Object obj) { }
    public virtual void MouseUp(UnityEngine.Object obj) { }
}