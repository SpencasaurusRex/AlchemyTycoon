using UnityEngine;

public class Tool : MonoBehaviour
{
    [Header("Configure")]
    public float[] Multipliers;
    public float[] Additions;

    public IngredientType AcceptedTypes;
    public IngredientType ResultType;

    ToolProcess process;

    void Start()
    {
        process = GetComponent<ToolProcess>();
    }
}