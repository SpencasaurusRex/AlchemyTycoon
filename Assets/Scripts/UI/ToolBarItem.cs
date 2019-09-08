using UnityEngine;
using UnityEngine.UI;

public class ToolBarItem : MonoBehaviour
{
    [Header("Configuration")]
    public GameObject SpawnItem;

    [Header("Runtime")]
    public KeyCode Key; // Dynamically determined in ToolBar

    ToolBar toolBar;
    Image img;
    RectTransform rectTransform;

    void Awake()
    {
        toolBar = GetComponentInParent<ToolBar>();
        img = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            toolBar.ItemSelected(this);
        }
    }

    public void Set(Sprite sprite, int size)
    {
        img.sprite = sprite;
        rectTransform.sizeDelta = new Vector2(size, size);
    }
}
