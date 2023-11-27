using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotsContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotsTemplate");
    }

    public void SetInventory(InventoryManager inventory)
    {
        this.inventoryManager = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
        
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 40f;

        foreach (Item item in inventoryManager.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("sprite").GetComponent<Image>();
            image.sprite = item.GetSprite();
            
            TextMeshProUGUI amountUItext = itemSlotRectTransform.Find("AmountText").GetComponent<TextMeshProUGUI>();
            if(item.amount > 1)
            {
                amountUItext.SetText(item.amount.ToString());
            }
            else
            {
                amountUItext.SetText(" ");
            }
            

            x++;

            if(x > 5)
            {
                x = 0;
                y++;
            }

        }
    }
}
