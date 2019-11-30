using UnityEngine;

public class DragReorder : MonoBehaviour
{
    public string HoldingLayer;

    Interactable interactable;
    // This won't work if number of layers changes
    string[] previousLayers;

    void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnStartDrag += StartDrag;
        interactable.OnDrop += DragRelease;
    }
    
    public void StartDrag()
    {
        transform.SetAsLastSibling();

        if (TryGetComponent<MultiLayer>(out var multiLayer))
        {
            previousLayers = new string[multiLayer.Layers.Length];
            for (int i = 0; i < multiLayer.Layers.Length; i++)
            {
                var sprite = multiLayer.Layers[i];
                previousLayers[i] = sprite.sortingLayerName;
                sprite.sortingLayerName = HoldingLayer;
            }
        }
        else if (TryGetComponent<SpriteRenderer>(out var sr))
        {
            previousLayers = new string[1];
            previousLayers[0] = sr.sortingLayerName;
            sr.sortingLayerName = HoldingLayer;
        }
    }

    public void DragRelease(GameObject _0, Interactable _1)
    {
        if (TryGetComponent<MultiLayer>(out var multiLayer))
        {
            for (int i = 0; i < multiLayer.Layers.Length; i++)
            {
                multiLayer.Layers[i].sortingLayerName = previousLayers[i];
            }
        }
        else if (TryGetComponent<SpriteRenderer>(out var sr))
        {
            sr.sortingLayerName = previousLayers[0];
        }
    }
}
