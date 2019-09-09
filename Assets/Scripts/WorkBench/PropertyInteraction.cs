using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu]
public class PropertyInteraction : ScriptableObject
{
    public IngredientProperty[] Inputs;
    public IngredientProperty[] Outputs;
}