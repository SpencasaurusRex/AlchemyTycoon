using UnityEngine;

public class ToolBar : MonoBehaviour
{
    [Header("Configuration")]
    public GameObject[] InventoryFrames;

    int SelectedIndex = 0;

    void Start()
    {
        Select(SelectedIndex);
    }

    void Select(int index)
    {

    }
}
