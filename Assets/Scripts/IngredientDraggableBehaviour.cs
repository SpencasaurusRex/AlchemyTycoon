using UnityEngine;

public class IngredientDraggableBehaviour : MonoBehaviour, IDraggable
{
    public bool ValidDrop(GameObject obj)
    {
        return true;
    }
}