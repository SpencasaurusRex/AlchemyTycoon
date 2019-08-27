using System;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string Name;
    public float[] Attributes;
    public PhysicalTrait Physical;

    void Awake()
    { 
        
    }
}

[Flags]
public enum PhysicalTrait
{
    Solid = 1,
    Powder = 2,
    Liquid = 4
}