using UnityEngine;

public class IngredientDrag : Clickable
{
    [Header("Configure")]
    public float InitialSharpness = 10;
    public float SharpnessIncrease = 2000f;
    public float MaxSharpness = 2000;
    [Header("Runtime")]
    public float Sharpness;
}