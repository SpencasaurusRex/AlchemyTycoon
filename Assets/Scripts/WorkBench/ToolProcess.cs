using UnityEngine;

[RequireComponent(typeof(Tool))]
public class ToolProcess : MonoBehaviour
{
    [Header("Configuration")]
    public float MaxProcessTime;

    float processTime;

    ToolUI toolUI;
    Tool tool;
    Ingredient ingredient;
    ProgressBar progressBar;

    void Start()
    {
        toolUI = GetComponentInChildren<ToolUI>();
        tool = GetComponent<Tool>();
        progressBar = GetComponentInChildren<ProgressBar>();
    }

    public bool CanProcess(Ingredient ingredient) => this.ingredient == null && (ingredient.IngredientType & tool.AcceptedTypes) > 0;

    public void StartProcess(Ingredient ingredient)
    {
        // Hide and move ingredient
        this.ingredient = ingredient;
        ingredient.GetComponent<SpriteRenderer>().enabled = false;
        ingredient.transform.position = transform.position;

        // Show progress bar
        toolUI.Processing(true);
    }

    void Update()
    {
        if (ingredient)
        {
            processTime += Time.deltaTime;
            progressBar.SetPercent(Mathf.Clamp01(processTime / MaxProcessTime));
            if (processTime >= MaxProcessTime)
            {
                FinishProcess();
                processTime = 0;
            }
        }
    }

    void FinishProcess()
    {
        ModifyIngredients();

        // Move and show ingredient
        ingredient.transform.position += Vector3.right;
        ingredient.GetComponent<SpriteRenderer>().enabled = true;
        ingredient = null;

        // Hide progress bar
        toolUI.Processing(false);
    }

    void ModifyIngredients()
    {
        for (int i = 0; i < ingredient.Attributes.Length; i++)
        {
            if (tool.Multipliers[i] == 0)
            {
                ingredient.Attributes[i] = tool.Additions[i];
                continue;
            }

            if (ingredient.Attributes[i] > 0)
            {
                ingredient.Attributes[i] = ingredient.Attributes[i] * tool.Multipliers[i] + tool.Additions[i];
            }
            else
            {
                ingredient.Attributes[i] = ingredient.Attributes[i] / tool.Multipliers[i] + tool.Additions[i];
            }
        }

        ingredient.SetType(tool.ResultType);
    }
}