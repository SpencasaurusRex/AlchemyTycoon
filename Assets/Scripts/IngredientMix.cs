using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Draggable))]
public class IngredientMix : MonoBehaviour
{
    [SerializeField]
    PhysicalState physicalState;
    public List<Ingredient> Ingredients;

    // Configuration
    public Color PowerColor;
    public Sprite PowderSprite;

    // Runtime
    SpriteRenderer sr;

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

    void Awake()
    {
        //GetComponent<Draggable>().behaviour.Result = this;
        sr = GetComponent<SpriteRenderer>();
    }

    public void Modify(AttributeAffector affector)
    {
        foreach (var ingredient in Ingredients)
        {
            if (ingredient.Attributes.Count > affector.Index)
            {
                ingredient.Attributes[affector.Index].Intensity += affector.Delta;
            }
        }
    }
}