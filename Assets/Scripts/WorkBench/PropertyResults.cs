using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu]
public class PropertyResults : ScriptableObject
{
    public IngredientProperty[] RequiredProperties;
    public IngredientProperty[] CannotHave;
}