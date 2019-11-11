using System.Collections.Generic;
using UnityEngine;

public class Cauldron: MonoBehaviour, IDragReceiver
{
    // Configuration
    public IngredientMix MixPrefab;
    public Transform IngredientParent;
    public Transform BottleParent;
    public Collision2DTrigger UICollider;
    public float ItemFloatRadius = 0.7f;
    public float ItemFloatSize = .8f;

    // Runtime
    Canvas canvas;
    List<IngredientMix> ingredientsHeld = new List<IngredientMix>();
    List<Bottle> bottlesHeld = new List<Bottle>();
    bool showingIngredients;
    float floatingRotationOffset;

    void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        showingIngredients = canvas.enabled;
        var interactable = GetComponent<Interactable>();
        interactable.OnReceive += Receive;
        interactable.OnClickRelease += Click;
        interactable.Register(this);
    }

    void Start()
    {
        UICollider.OnTriggerEnter += UIEnter;
        UICollider.OnTriggerExit += UIExit;
        print("Cauldron Start()");
    }

    void UIEnter(Collider2D collider)
    {
        if (!showingIngredients) return;
        if (collider.TryGetComponent<Interactable>(out var interactable))
        {
            print("Registering");
            interactable.OnDrop += UIDropped;
        }
    }

    void UIExit(Collider2D collider)
    {
        if (collider.TryGetComponent<Interactable>(out var interactable))
        {
            print("Unregistering");
            interactable.OnDrop -= UIDropped;
        }
        if (!collider.gameObject.activeSelf) return;
        Release(collider.gameObject);
    }

    public void UIDropped(GameObject obj, Interactable _)
    {
        Receive(obj);
    }

    public void Click()
    {
        DisplayIngredients();
        //MixIngredients();
    }

    public void DisplayIngredients()
    {
        print("DisplayIngredients()");
        canvas.enabled = !canvas.enabled;

        showingIngredients = canvas.enabled;

        int totalItems = ingredientsHeld.Count + bottlesHeld.Count;
        var positions = GetRotatingPositions();

        if (showingIngredients)
        {
            for (int i = 0; i < totalItems; i++)
            {
                if (i < ingredientsHeld.Count)
                {
                    ingredientsHeld[i].transform.position = positions[i];
                }
                else
                {
                    var bottle = bottlesHeld[i - ingredientsHeld.Count];
                    bottle.transform.position = positions[i];
                }
            }
        }

        foreach (var ingredient in ingredientsHeld)
        {
            //ingredient.gameObject.GetComponent<Interactable>().SetEnabled(showingIngredients);
            ingredient.gameObject.SetActive(showingIngredients);
        }

        foreach (var bottle in bottlesHeld)
        {
            bottle.gameObject.SetActive(showingIngredients);
            //bottle.gameObject.GetComponent<Interactable>().SetEnabled(showingIngredients);
        }
    }

    public void MixIngredients()
    {
        IngredientMix result = Instantiate(MixPrefab, IngredientParent);
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

        foreach (var bottle in bottlesHeld)
        {
            Destroy(bottle.gameObject);
        }
        foreach (var ingredient in ingredientsHeld)
        {
            Destroy(ingredient.gameObject);
        }

        bottlesHeld.Clear();
        ingredientsHeld.Clear();
    }

    void Update()
    {
        if (showingIngredients)
        {
            // This might be the last audio test that were going to do
            // And this is me typing and talking at the same time

            floatingRotationOffset += Time.deltaTime * .3f;
            int totalItems = ingredientsHeld.Count + bottlesHeld.Count;
            var positions = GetRotatingPositions();

            for (int i = 0; i < totalItems; i++)
            {
                if (i < ingredientsHeld.Count)
                {
                    ingredientsHeld[i].transform.position  
                        = Vector3.Lerp(ingredientsHeld[i].transform.position, positions[i], .2f);
                }
                else
                {
                    var bottle = bottlesHeld[i - ingredientsHeld.Count];
                    bottle.transform.position
                        = Vector3.Lerp(bottle.transform.position, positions[i], .2f);
                }
            }
        }
    }

    Vector2[] GetRotatingPositions()
    {
        int totalItems = ingredientsHeld.Count + bottlesHeld.Count;

        Vector2[] positions = new Vector2[totalItems];
        for (int i = 0; i < totalItems; i++)
        {
            float theta = Mathf.PI * 2 / totalItems * i + floatingRotationOffset;
            positions[i] = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)) * ItemFloatRadius + (Vector2)transform.position;
        }

        return positions;
    }

    public void Receive(GameObject obj)
    {
        var bottle = obj.GetComponent<Bottle>();
        var ingredientMix = obj.GetComponent<IngredientMix>();

        if (bottle != null)
        {
            if (bottlesHeld.Contains(bottle)) return;
            bottlesHeld.Add(bottle);
        }
        else if (ingredientMix != null)
        {
            if (ingredientsHeld.Contains(ingredientMix)) return;
            ingredientsHeld.Add(ingredientMix);
        }
        else return;

        if (!showingIngredients)
        {
            obj.SetActive(false);
        }

        //obj.transform.position = new Vector2(0, 0);
        obj.transform.SetParent(this.transform, true);
        obj.transform.localScale = new Vector2(ItemFloatSize, ItemFloatSize);
    }

    public void Release(GameObject obj)
    {
        var bottle = obj.GetComponent<Bottle>();
        var ingredientMix = obj.GetComponent<IngredientMix>();

        if (bottle != null)
        {
            bottlesHeld.Remove(bottle);
            bottle.transform.parent = BottleParent;
        }
        else if (ingredientMix != null)
        {
            ingredientsHeld.Remove(ingredientMix);
            ingredientMix.transform.parent = IngredientParent;
        }
        else return;

        obj.transform.localScale = new Vector2(1, 1);
    }

    public bool CanReceive(GameObject obj)
    {
        return obj.GetComponent<Bottle>() != null || obj.GetComponent<IngredientMix>() != null;
    }
}
