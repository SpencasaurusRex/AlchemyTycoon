public class Ingredient
{
    IngredientType Type;
    float quality;

    public Ingredient(IngredientType type)
    {
        Type = type;
    }
}

public enum IngredientType
{
    ArrowRoot,
    BlueToadshade,
    BloodLeaf,
    LavenderSprig,
    WyrmtonguePetals,
    AmanitaCap,
    CactusJuice,
    MilkweedSeeds,
    BloodGrass
}