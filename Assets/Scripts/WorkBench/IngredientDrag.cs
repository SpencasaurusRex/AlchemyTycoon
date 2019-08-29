using UnityEngine;

public class IngredientDrag : MonoBehaviour
{
    [Header("Configure")]
    public float InitialSharpness = 10;
    public float SharpnessIncrease = 2000f;
    public float MaxSharpness = 2000;
    [Header("Runtime")]
    public float Sharpness;

    BoxCollider2D collider;

    void Start()
    {
        Sharpness = InitialSharpness;
        collider = GetComponent<BoxCollider2D>();
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
        Vector2 origin = transform.position + collider.offset.WithZ(0);
        // Physics2D.OverlapBoxAll(origin, collider.size, 0, LayerMask.GetMask(""));
        var hits = Physics2D.RaycastAll(origin, Vector2.zero, 0, LayerMask.GetMask("Tool"));
        foreach (var hit in hits) print(hit.collider.gameObject.name);
    }

    public Vector3 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition).WithZ(0);
}