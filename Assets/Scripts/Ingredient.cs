using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Draggable))]
public class Ingredient : MonoBehaviour, IDraggable
{
    [SerializeField]
    PhysicalState physicalState;

    public PhysicalState PhysicalState
    {
        get => physicalState;
        set
        {
            sr.sprite = PowderSprite;
            sr.color = PowerColor;
            physicalState = value;
        }
    }

    public List<IngredientAttribute> Attributes;
    public Color PowerColor;
    public Sprite PowderSprite;

    // Runtime
    SpriteRenderer sr;

    void Awake()
    {
        GetComponent<Draggable>().behaviour.Result = this;
        sr = GetComponent<SpriteRenderer>();
    }

    public void StartDrag()
    {
    }

    public void Drop(DropReceiver receiver)
    {
    }

    public void Reorder(int siblingIndex)
    {
    }
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