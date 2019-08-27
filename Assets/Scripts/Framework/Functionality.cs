using System;
using System.Collections.Generic;

public abstract class Functionality
{ 
    public Type[] SubscribedComponents;

    public Functionality(params Type[] components)
    { 
        SubscribedComponents = components;
    }

    public virtual void Update(List<UnityEngine.Object> objects) { }
    public virtual void FixedUpdate(List<UnityEngine.Object> objects) { }
    public virtual void MouseDown(UnityEngine.Object obj) { }
    public virtual void MouseDrag(UnityEngine.Object obj) { }
    public virtual void MouseUp(UnityEngine.Object obj) { }
}