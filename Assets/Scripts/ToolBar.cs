﻿using Sirenix.OdinInspector;
using System;
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
    int SelectedIndex = 0;

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
        }

        item.Set(SelectedSprite, SelectedSize);
        SelectedIndex = Array.IndexOf(InventoryFrames, item);
    }
}
