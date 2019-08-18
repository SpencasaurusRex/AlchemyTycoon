using TMPro;
using UnityEngine;

public class ToolObject : MonoBehaviour
{
    // Initialization
    public string ToolName;

    // Runtime fields
    public ITool Tool;
    public Ingredient Ingredient;
    public float TimeUntilProcessed;

    void Awake()
    {
        Tool = ToolFactory.Get(ToolName);

        var tmp = GetComponentInChildren<TextMeshProUGUI>();
        if (tmp != null) tmp.text = Tool.Name;
    }

    public bool IngredientDropped(Ingredient ingredient)
    { 
        if (!Tool.CanProcess(ingredient))
        { 
            return false;
        }
        
        Ingredient = ingredient;
        return true;
    }
}