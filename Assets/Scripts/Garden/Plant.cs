using Sirenix.OdinInspector;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("Configuration")]
    public float MaxHydration;
    public Range HealthyHydration;
    public float HydrationLoss;
    public float MaxGrowth;
    public float StartingGrowth;
    public GameObject ResultingIngredient;
    [Required]
    public Sprite[] GrowthSprites;
    [Required]
    public SpriteRenderer PlantSprite;

    [Header("Runtime")]
    public float Hydration;
    public float Growth;
    public float Health; // 0 to 1

    void Start()
    {
        Hydration = MaxHydration * .5f;
        Growth = StartingGrowth;
        Health = .5f;
    }

    void Update()
    {
        Growth += Time.deltaTime;
        Hydration -= Time.deltaTime;
        if (Hydration <= HealthyHydration.Max && Hydration >= HealthyHydration.Min)
        {
            Health += .5f * Time.deltaTime / MaxGrowth;
        }
        else if (Hydration > HealthyHydration.Max)
        {
            Health -= .5f * Time.deltaTime / MaxGrowth;
        }
        else
        {
            Health -= Time.deltaTime / MaxGrowth;
        }

        int targetSpriteIndex = Mathf.FloorToInt(Growth * (GrowthSprites.Length - 1) / MaxGrowth);
        targetSpriteIndex = Mathf.Clamp(targetSpriteIndex, 0, GrowthSprites.Length - 1);
        Sprite targetSprite = GrowthSprites[targetSpriteIndex];

        if (targetSprite != PlantSprite.sprite)
        {
            PlantSprite.sprite = targetSprite;
        }
    }
}