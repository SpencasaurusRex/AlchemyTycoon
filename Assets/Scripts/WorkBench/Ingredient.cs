using System;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [Header("Configure")]
    public float[] Attributes;
    public PhysicalTrait StartingPhysical;
    [Header("Runtime")]
    public PhysicalTrait Physical;

    void Start()
    {
        Physical = StartingPhysical;
    }
}

[Flags]
public enum PhysicalTrait
{
    Solid = 1,
    Powder = 2,
    Liquid = 4
}