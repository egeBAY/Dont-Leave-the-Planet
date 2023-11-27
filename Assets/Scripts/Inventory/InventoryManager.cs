using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager
{
    public List<Item> itemList { get; private set; }

    public event EventHandler OnItemListChanged;

    public InventoryManager()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.IceResource, amount = 3});
        AddItem(new Item { itemType = Item.ItemType.Gun, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Sword, amount = 1 });

        Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach(Item inventoryItem in itemList)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

}
