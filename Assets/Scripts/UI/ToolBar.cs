using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ToolBar : MonoBehaviour
{
    [Header("Configuration")]
    public ToolBarItem[] InventoryFrames;
    [Required]
    public Sprite UnselectedSprite;
    [Required]
    public Sprite SelectedSprite;
    public int SelectedSize;
    public int UnselectedSize;

    GameObject handItem;
    SceneObject sceneObject;
    int SelectedIndex = 0;

    void Awake()
    {
        sceneObject = GetComponent<SceneObject>();
        sceneObject.Disable.Add(this);
    }

    void Start()
    {
        KeyCode key = KeyCode.Alpha1;
        foreach (var frames in InventoryFrames)
        {
            frames.Key = key++;
        }

        ItemSelected(InventoryFrames[SelectedIndex]);
    }

    public void ItemSelected(ToolBarItem item)
    {
        InventoryFrames[SelectedIndex].Set(UnselectedSprite, UnselectedSize);

        if (handItem != null)
        {
            Destroy(handItem);
        }
        if (item.SpawnItem != null)
        {
            handItem = Instantiate(item.SpawnItem);
            handItem.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition).WithZ(0);
        }

        item.Set(SelectedSprite, SelectedSize);
        SelectedIndex = Array.IndexOf(InventoryFrames, item);
    }
}
