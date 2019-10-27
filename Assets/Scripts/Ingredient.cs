using System;
using UnityEngine;

[RequireComponent(typeof(Draggable))]
public class Ingredient : MonoBehaviour, IDraggable
{
    public PhysicalState PhysicalState;

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