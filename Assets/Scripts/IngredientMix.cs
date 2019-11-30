using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class IngredientMix : MonoBehaviour
{
    [SerializeField]
    PhysicalState physicalState;
    public List<Ingredient> Ingredients;

    // Configuration
    public Color PowderColor;
    public Sprite PowderSprite;

    // Runtime
    SpriteRenderer sr;

    public PhysicalState PhysicalState
    {
        get => physicalState;
        set
        {
            sr.sprite = PowderSprite;
            sr.color = PowderColor;
            physicalState = value;
        }
    }

    public void SetColor(Color color)
    {
        var sr = GetComponent<SpriteRenderer>();
        sr.color = color;
        PowderColor = color;
    }

    void Awake()
    {
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