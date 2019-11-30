using System.Linq;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    void Update()
    {
        AssignOrderInLayer();
    }

    void AssignOrderInLayer()
    {
        int orderInLayer = 0;

        for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
        {
            var child = transform.GetChild(childIndex);

            if (child.TryGetComponent<MultiLayer>(out var multiLayer))
            {
                foreach (var layer in multiLayer.Layers)
                {
                    layer.sortingOrder = orderInLayer++;
                }
            }
            else if (child.TryGetComponent<SpriteRenderer>(out var sr))
            {
                sr.sortingOrder = orderInLayer++;
            }
        }
    }
}