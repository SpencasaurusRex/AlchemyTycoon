using Sirenix.OdinInspector;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [Header("Configure")]
    public float[] Multipliers;
    public float[] Additions;

    public IngredientType AcceptedTypes;
    public Color Tint;
    public ProcessType ProcessType;

    [ShowIf("ProcessType", ProcessType.OneResultType)]
    public IngredientType ResultType;

    ToolProcess process;

    void Start()
    {
        process = GetComponent<ToolProcess>();
    }
 }

public enum ProcessType
{
    OneResultType,
    NoChange
}