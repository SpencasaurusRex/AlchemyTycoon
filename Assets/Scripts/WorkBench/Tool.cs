﻿using UnityEngine;

public class Tool : MonoBehaviour
{
    [Header("Configure")]
    public float[] Multipliers;
    public float[] Additions;
    public PhysicalTrait AcceptedPhysical;
    public PhysicalTrait ResultingPhysical;

    Ingredient ingredient;
    public Ingredient Ingredient
    {
        set
        {
            value.GetComponent<SpriteRenderer>().enabled = false;
            ingredient = value;
        }

        get => ingredient;
    }

    public bool CanProcess(Ingredient ingredient) => (ingredient.Physical & AcceptedPhysical) > 0;

    public void Process(Ingredient ingredient)
    {
        for (int i = 0; i < ingredient.Attributes.Length; i++)
        {
            if (Multipliers[i] == 0)
            {
                ingredient.Attributes[i] = Additions[i];
                continue;
            }

            if (ingredient.Attributes[i] > 0)
            {
                ingredient.Attributes[i] = ingredient.Attributes[i] * Multipliers[i] + Additions[i];
            }
            else
            {
                ingredient.Attributes[i] = ingredient.Attributes[i] / Multipliers[i] + Additions[i];
            }
        }
    }
}