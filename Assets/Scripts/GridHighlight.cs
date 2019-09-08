using UnityEngine;

public class GridHighlight : MonoBehaviour
{
    [Header("Configuration")]
    public float Sharpness;

    Vector2 target = new Vector2();

    void Start()
    {
        // TODO
    }

    void Update()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.x = Mathf.Round(worldPoint.x);
        target.y = Mathf.Round(worldPoint.y);

        transform.position = Vector2.Lerp(transform.position, target, 1 - Mathf.Exp(-Sharpness * Time.deltaTime));
    }
}
