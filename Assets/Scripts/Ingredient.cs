using UnityEngine;

[RequireComponent(typeof(Draggable))]
public class Ingredient : MonoBehaviour, IDraggable
{
    public bool CanDropOn(GameObject obj)
    {
        return true;
    }

    void Awake() 
    {
        GetComponent<Draggable>().behaviour.Result = this;
    }
}