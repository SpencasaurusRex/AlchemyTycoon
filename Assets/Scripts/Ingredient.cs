using System;

public class Ingredient
{
    public string Name;
    public float[] Attributes;
    public PhysicalTrait Physical;
}

public enum IngredientType
{
    MilkWeedSeeds,
    AmanitaCap,
    ArrowRoot,
    BloodGrass,
    LavenderSprig,
    BlueToadshade
}

[FlagsAttribute]
public enum PhysicalTrait
{
    Solid = 1,
    Powder = 2,
    Liquid = 4
}

public static class IngredientTypeExtensions
{
    static readonly float[][] IngredientAttributes =
    {
        new [] {.4f, -.4f, 0},
        new [] {.4f, 0, -.4f},
        new [] { 0f, 1,-1},
        new [] {-1f, 1, 0},
        new [] {-1f, 0, 1},
        new [] { 0f,-1, 1},
    };

    public static float[] Attributes(this IngredientType type)
    {
        return IngredientAttributes[(int)type];
    }

    public static string Name(this IngredientType type)
    {
        switch (type)
        {
            case IngredientType.MilkWeedSeeds:
                return "Milkweed Seeds";
            case IngredientType.AmanitaCap:
                return "Amanita Cap";
            case IngredientType.ArrowRoot:
                return "Arrowroot";
            case IngredientType.BloodGrass:
                return "Bloodgrass";
            case IngredientType.LavenderSprig:
                return "Lavender Sprig";
            case IngredientType.BlueToadshade:
                return "Blue Toadshade";
        }
        throw new ArgumentOutOfRangeException();
    }
}