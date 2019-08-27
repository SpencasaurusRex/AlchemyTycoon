using System;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : Functionality
{
    public HideObject(params Type[] components) : base(typeof(SpriteRenderer))
    {
    }

    public override void Update(List<UnityEngine.Object> objects)
    { 
        foreach(var obj in objects)
        { 
            ((SpriteRenderer)obj).enabled = !((SpriteRenderer)obj).enabled;
        }
    }
}