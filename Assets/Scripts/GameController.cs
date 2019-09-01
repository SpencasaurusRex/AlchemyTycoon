using Sirenix.OdinInspector;
using System.Collections.Generic;

public class GameController : SerializedMonoBehaviour
{
    public static GameController Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    #region Ingredient Properties
    

    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine)]
    public Dictionary<IngredientType, IngredientProperty> IngredientProperties = new Dictionary<IngredientType, IngredientProperty>()
    {
        { IngredientType.FlyAmanita, new IngredientProperty() },
        { IngredientType.MilkWeed, new IngredientProperty() },
        { IngredientType.Powder, new IngredientProperty() },
    };

    #endregion
}

public enum IngredientType
{
    FlyAmanita,
    MilkWeed,
    Powder,
}