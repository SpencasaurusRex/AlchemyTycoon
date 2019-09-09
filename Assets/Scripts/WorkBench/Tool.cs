using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [Header("Configure")]
    public PropertyChange[] Changes;

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

[Serializable]
public class PropertyChange
{
    public IngredientProperty From;
    public IngredientProperty To;
}

public enum ProcessType
{
    OneResultType,
    NoChange
}