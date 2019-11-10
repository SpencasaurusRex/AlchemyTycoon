using UnityEngine;

public class Bottle : MonoBehaviour
{
    SpriteRenderer sprite;
    public SpriteRenderer childSprite;
    public string Liquid;
    public int Intensity;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        GetComponent<Interactable>().OnStartDrag += StartDrag;
    }

    public void StartDrag()
    {
    }

    public void Drop(/*DropReceiver obj*/)
    {
        
    }

    public void Reorder(int index)
    {
    }
}
