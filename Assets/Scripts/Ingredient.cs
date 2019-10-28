using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Draggable))]
public class Ingredient : MonoBehaviour, IDraggable
{
    public PhysicalState PhysicalState;

    public List<IngredientAttribute> Attributes;

    public bool CanDropOn(GameObject obj)
    {
        return true;
    }

    void Awake() 
    {
        GetComponent<Draggable>().behaviour.Result = this;
    }   
}

[Flags]
public enum PhysicalState
{
    Raw = 1,
    Powder = 2,
    Bottle = 4
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