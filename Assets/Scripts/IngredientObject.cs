using System;
using UnityEngine;

public class IngredientObject : MonoBehaviour
{
    public float[] Attributes;
    public PhysicalTrait Physical;
}

[Flags]
public enum PhysicalTrait
{
    Solid = 1,
    Powder = 2,
    Liquid = 4
}