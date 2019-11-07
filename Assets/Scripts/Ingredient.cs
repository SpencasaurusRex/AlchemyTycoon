using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Ingredient
{
    public List<IngredientAttribute> Attributes;
}

[Flags]
[Serializable]
public enum PhysicalState
{
    Raw,
    Powder,
    Bottle
}

[Serializable]
public class IngredientAttribute
{
    const int MAX_INTENSITY = 3;
    [SerializeField]
    int intensity;

    public string Type;
    public int Intensity
    {
        get => intensity;

        set
        {
            intensity = Mathf.Clamp(value, 0, MAX_INTENSITY);
        }
    }

    public bool Unlocked;
}