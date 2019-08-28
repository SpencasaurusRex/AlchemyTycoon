using UnityEngine;

public class DragIngredient : Functionality
{
    public DragIngredient() : base(typeof(IngredientDrag))
    {
    }

    public override void MouseDown(Object obj)
    {
        
    }

    public override void MouseDrag(Object obj)
    {
        var o = Cast<IngredientDrag>(obj);
        o.Sharpness = Mathf.Clamp(o.Sharpness + o.SharpnessIncrease * Time.deltaTime, o.InitialSharpness, o.MaxSharpness);
        
        // Lerp ingredient to mouse position
        float factor =  1 - Mathf.Exp(-o.Sharpness * Time.deltaTime);
        o.transform.position = Vector3.Lerp(o.transform.position, MousePosition, factor);
    }

    public override void MouseUp(Object obj)
    {
        var o = Cast<IngredientDrag>(obj);
        o.Sharpness = o.InitialSharpness;

        // Figure out where the ingredient was dropped

    }
}