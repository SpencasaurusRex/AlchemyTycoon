using System.Collections.Generic;
using UnityEngine;

public class Cauldron: MonoBehaviour, IDragReceiver
{
    // Configuration
    public IngredientMix MixPrefab;
    public Transform IngredientParent;
    public Collision2DTrigger UICollider;

    public float MaxClickTime = 0.2f;

    // Runtime
    Canvas canvas;
    List<IngredientMix> ingredientsHeld = new List<IngredientMix>();
    List<Bottle> bottlesHeld = new List<Bottle>();
    bool showingIngredients;
    CircleMover circleMover;
    Interactable interactable;
    float maxClickTime;

    void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        showingIngredients = canvas.enabled;
        circleMover = GetComponent<CircleMover>();
        circleMover.OnAnimationComplete += MixIngredients;
        interactable = GetComponent<Interactable>();
        interactable.OnReceive += Receive;
        interactable.OnClickRelease += ClickRelease;
        interactable.OnClickHold += ClickHold;
        interactable.OnStartDrag += StartDrag;
        interactable.Register(this);
    }

    void Start()
    {
        UICollider.OnTriggerEnter += UIEnter;
        UICollider.OnTriggerExit += UIExit;
    }

    void UIEnter(Collider2D collider)
    {
        if (!showingIngredients) return;
        if (collider.TryGetComponent<Interactable>(out var interactable))
        {
            interactable.OnDrop += UIDropped;
        }
    }

    void UIExit(Collider2D collider)
    {
        if (collider.TryGetComponent<Interactable>(out var interactable))
        {
            interactable.OnDrop -= UIDropped;
        }
        if (!collider.gameObject.activeSelf) return;
        Release(collider.gameObject);
    }

    public void UIDropped(GameObject obj, Interactable _)
    {
        Receive(obj);
    }

    public void ClickRelease(float totalTime)
    {
        if (totalTime < MaxClickTime)
        {
            // Stop clicking
            DisplayIngredients();
        }
        circleMover.StopAnimation();
        interactable.Draggable = true;
    }

    public void ClickHold(float totalTime)
    {
        if (totalTime > MaxClickTime)
        {
            circleMover.StartAnimation();
            interactable.Draggable = false;
        }
    }

    public void StartDrag()
    {
        circleMover.StopAnimation();
    }

    public void DisplayIngredients()
    {
        canvas.enabled = !canvas.enabled;
        showingIngredients = canvas.enabled;
        circleMover.SetVisible(showingIngredients);
    }

    public void MixIngredients()
    {
        IngredientMix result = Instantiate(MixPrefab, IngredientParent);
        result.transform.position = transform.position + new Vector3(1, 0, 0);

        Color averageColor = new Color(0, 0, 0);

        foreach (var ingredientMix in ingredientsHeld)
        {
            averageColor += ingredientMix.PowderColor;
            foreach (var ingredient in ingredientMix.Ingredients)
            {
                result.Ingredients.Add(ingredient);
            }
        }

        result.SetColor(averageColor / ingredientsHeld.Count);

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

        for (int i = bottlesHeld.Count - 1; i >= 0; i--)
        {
            Destroy(bottlesHeld[i].gameObject);
        }
        for (int i = ingredientsHeld.Count - 1; i >= 0; i--)
        {
            Destroy(ingredientsHeld[i].gameObject);
        }

        bottlesHeld.Clear();
        ingredientsHeld.Clear();

        circleMover.ClearTransforms();
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

        circleMover.AddTransform(obj.transform);
    }

    public void Release(GameObject obj)
    {
        var bottle = obj.GetComponent<Bottle>();
        var ingredientMix = obj.GetComponent<IngredientMix>();
        obj.transform.parent = IngredientParent;

        circleMover.RemoveTransform(obj.transform);

        if (bottle != null)
        {
            bottlesHeld.Remove(bottle);
        }
        else if (ingredientMix != null)
        {
            ingredientsHeld.Remove(ingredientMix);
        }
        else return;

        obj.transform.localScale = new Vector2(1, 1);
    }

    public bool CanReceive(GameObject obj)
    {
        return obj.GetComponent<Bottle>() != null || obj.GetComponent<IngredientMix>() != null;
    }
}
