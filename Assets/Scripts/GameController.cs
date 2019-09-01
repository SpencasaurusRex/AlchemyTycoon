using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SerializedMonoBehaviour
{
    public static GameController Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    #region Ingredient Properties
    
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine)]
    public Dictionary<IngredientType, IngredientProperty> IngredientProperties = new Dictionary<IngredientType, IngredientProperty>()
    {
        { IngredientType.FlyAmanita, new IngredientProperty() },
        { IngredientType.MilkWeed, new IngredientProperty() },
        { IngredientType.Powder, new IngredientProperty() },
    };

    public Sprite GetSprite(IngredientType type)
    {
        return IngredientProperties[type].Sprite;
    }

    #endregion
}

[Flags]
public enum IngredientType
{
    MilkWeed = 1,
    FlyAmanita = 2,
    ArrowRoot = 4,
    BloodGrass = 8,
    Lavender = 16,
    ToadLegs = 32,
    Powder = 64,
    Bottle = 128
}