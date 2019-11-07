using System.Collections.Generic;
using UnityEngine;

public class Cauldron: MonoBehaviour, IClickable, IDropReceiver
{
    // Configuration
    public IngredientMix MixPrefab;

    // Runtime
    Canvas canvas;
    List<IngredientMix> ingredientsHeld = new List<IngredientMix>();
    List<Bottle> bottlesHeld = new List<Bottle>();

    void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
    }

    void Start()
    {
        GetComponent<Clickable>().behaviour.Result = this;
        GetComponent<DropReceiver>().behaviour.Result = this;
    }

    public bool Click()
    {
        //canvas.enabled = !canvas.enabled;
        // Process the ingredients
        IngredientMix result = Instantiate(MixPrefab);
        result.transform.position = transform.position + new Vector3(1, 0, 0);

        foreach (var ingredientMix in ingredientsHeld)
        {
            foreach (var ingredient in ingredientMix.Ingredients)
            {
                result.Ingredients.Add(ingredient);
            }
        }

        foreach (var bottle in bottlesHeld)
        {
            foreach (var ingredient in result.Ingredients)
            {
                for (int i = 0; i < ingredient.Attributes.Count; i++)
                {
                    if (bottle.Intensity > i)
                    {
                        ingredient.Attributes[i].Unlocked = true;
                    }
                }
            }
        }

        return true;
    }

    void Update()
    {

    }

    public bool Receive(GameObject obj)
    {
        var bottle = obj.GetComponent<Bottle>();
        var ingredientMix = obj.GetComponent<IngredientMix>();

        if (bottle != null)
        {
            bottlesHeld.Add(bottle);
        }
        else if (ingredientMix != null)
        {
            ingredientsHeld.Add(ingredientMix);
        }

        obj.SetActive(false);

        return true;
    }
}
