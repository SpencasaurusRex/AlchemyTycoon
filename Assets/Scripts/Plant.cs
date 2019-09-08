using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("Configuration")]
    public float MaxHydration;
    public Range HealthyHydration;
    public float HydrationLoss;
    public float MaxGrowth;
    public GameObject ResultingIngredient;

    [Header("Runtime")]
    public float Hydration;
    public float Growth;
    public float Health; // 0 to 1
    public PlantState state;

    void Start()
    {
        
    }

    public void PlantSeed()
    {
        Hydration = MaxHydration * .5f;
        Growth = 0;
        Health = .5f;
    }

    void Update()
    {
        if (state == PlantState.NotPlanted) return;

        Growth += Time.deltaTime;
        Hydration -= Time.deltaTime;
    }
}

public enum PlantState
{
    NotPlanted,
    Planted
}
