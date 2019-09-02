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
    public SceneObject SceneObject;

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SceneObject = GetComponent<SceneObject>();
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