using UnityEngine;

[RequireComponent(typeof(DropReceiver))]
public class Tool : MonoBehaviour, IDropReceiver
{
    public bool Receive(GameObject obj)
    {
        print("Receiving " + obj.name);
        return obj.GetComponent<Ingredient>() != null;
    }

    void Awake()
    {
        GetComponent<DropReceiver>().behaviour.Result = this;
    }
}