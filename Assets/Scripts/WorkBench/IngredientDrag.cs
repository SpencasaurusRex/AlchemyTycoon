using Sirenix.OdinInspector;
using UnityEngine;

public class IngredientDrag : MonoBehaviour
{
    [Header("Configure")]
    public float InitialSharpness = 10;
    public float SharpnessIncrease = 2000f;
    public float MaxSharpness = 2000;
    [Header("Runtime")]
    [DisableInEditorMode]
    public float Sharpness;
    BoxCollider2D boxCollider;
    Ingredient ingredient;
    Vector2 startPosition;
    void Start()
    {
        Sharpness = InitialSharpness;
        boxCollider = GetComponent<BoxCollider2D>();
        ingredient = GetComponent<Ingredient>();
    }

    void OnMouseDown()
    {
        startPosition = transform.position;
    }

    void OnMouseDrag()
    {
        Sharpness = Mathf.Clamp(Sharpness + SharpnessIncrease * Time.deltaTime, InitialSharpness, MaxSharpness);

        // Lerp ingredient to mouse position
        float factor = 1 - Mathf.Exp(-Sharpness * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, MousePosition, factor);
    }

    void OnMouseUp()
    {
        Sharpness = InitialSharpness;

        // Figure out where we dropped
        Vector2 origin = transform.position + boxCollider.offset.WithZ(0);
        // Physics2D.OverlapBoxAll(origin, collider.size, 0, LayerMask.GetMask(""));
        var hit = Physics2D.Raycast(MousePosition, Vector2.zero, 0, LayerMask.GetMask("Tool"));
        if (hit == false) return;

        var tool = hit.collider.gameObject.GetComponent<ToolProcess>();
        if (tool.CanProcess(ingredient))
        {
            tool.StartProcess(ingredient);
        }
        else
        {
            transform.position = startPosition;
        }
    }

    public Vector3 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition).WithZ(0);
}