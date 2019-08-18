using UnityEngine;

public class InputManager : MonoBehaviour
{ 
    public static InputManager Instance { get; set; }

    GameObject InHand;
    HeldType Type;

    void Start()
    { 
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
    }
}

public enum HeldType
{
    Ingredient,
    Tool
}