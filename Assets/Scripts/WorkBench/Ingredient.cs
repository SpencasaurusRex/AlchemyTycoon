using Sirenix.OdinInspector;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [Header("Configure")]
    public float[] Attributes;
    public IngredientType StartIngredientType;

    [Header("Runtime")]
    [DisableInEditorMode]
    public IngredientType IngredientType;

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        SetType(StartIngredientType);
    }

    public void SetType(IngredientType type)
    {
        IngredientType = type;
        sr.sprite = GameController.Instance.GetSprite(type);
    }

    public void Tint(Color color)
    {
        sr.color = sr.color * color;
    }
}