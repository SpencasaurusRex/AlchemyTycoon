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

    public bool PlantSeed()
    {
        if (state == PlantState.Planted) return false;

        Hydration = MaxHydration * .5f;
        Growth = 0;
        Health = .5f;
        state = PlantState.Planted;
        return true;
    }

    void Update()
    {
        if (state == PlantState.NotPlanted) return;

        Growth += Time.deltaTime;
        Hydration -= Time.deltaTime;
        if (Hydration <= HealthyHydration.Max && Hydration >= HealthyHydration.Min)
        {
            Health += .5f * Time.deltaTime / MaxGrowth;
        }
        else
        {
            Health -= Time.deltaTime / MaxGrowth;
        }
    }
}

public enum PlantState
{
    NotPlanted,
    Planted
}
