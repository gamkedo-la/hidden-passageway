﻿using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    Item item;

    public Image icon;
    public Button removeButton;
    public KeyCode useKey;
    public KeyCode keyAlt;

	private void Update()
	{
        if (Input.GetKeyDown(useKey) || Input.GetKeyDown(keyAlt))
        {
            UseItem();
        }
	}

	public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        if(item != null)
        {
            item.RemoveItem();
            Inventory.instance.RemoveItem(item);
        }
    }

    public void UseItem()
    {
        if(item != null)
        {
            if(item.Use(item.name))
            {
                item.RemoveItem();
                Inventory.instance.RemoveItem(item);
            }
        }
    }
}
