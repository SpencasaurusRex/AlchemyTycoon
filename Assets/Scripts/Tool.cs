using TMPro;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public ToolType Type;

    public Ingredient Ingredient;
    public float TimeUntilProcessed;

    void Awake()
    {
        var tmp = GetComponentInChildren<TextMeshPro>();
        tmp.text = Type.ToString();
    }
}

public enum ToolType
{
    Alembic,
    Retort,
    Rack,
    Burn,
    Freezer,
    Barrel
}

public static class ToolExensions
{
    public static int[][] ToolAttributes =
    {
        new [] { 1, -1,  0},
        new [] { 1,  0, -1},
        new [] { 0,  1, -1},
        new [] {-1,  1,  0},
        new [] {-1,  0,  1},
        new [] { 0, -1,  1}
    };

    public static int[] Attributes(this ToolType tool)
    {
        return ToolAttributes[(int) tool];
    }
}