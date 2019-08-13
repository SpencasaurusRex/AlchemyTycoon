using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string Name;
    public float[] Attributes;
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

public static class IngredientTypeExtensions
{
    static readonly int[][] IngredientAttributes =
    {
        new [] { 1, -1,  0},
        new [] { 1,  0, -1},
        new [] { 0,  1, -1},
        new [] {-1,  1,  0},
        new [] {-1,  0,  1},
        new [] { 0, -1,  1},
    };

    public static int[] Attributes(this IngredientType type)
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
            default:
                return "Mixture";
        }
    }
}