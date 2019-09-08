using Sirenix.OdinInspector;
using UnityEngine;

public class SeedItem : MonoBehaviour
{
    [AssetsOnly]
    public GameObject PlantSpawn;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).Round();
            
            var hit = Physics2D.Raycast(worldPos, Vector2.zero, 0, LayerMask.GetMask("GardenObject"));
            if (!hit)
            {
                GameObject go = Instantiate(PlantSpawn);
                go.transform.position = worldPos;
            }
        }
    }
}
