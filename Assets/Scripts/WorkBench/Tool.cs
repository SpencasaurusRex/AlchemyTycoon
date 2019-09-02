using UnityEngine;

public class Tool : MonoBehaviour
{
    [Header("Configure")]
    public float[] Multipliers;
    public float[] Additions;
    public PhysicalTrait AcceptedPhysical;
    public PhysicalTrait ResultingPhysical;

    ToolProcess process;

    void Start()
    {
        process = GetComponent<ToolProcess>();
    }
}