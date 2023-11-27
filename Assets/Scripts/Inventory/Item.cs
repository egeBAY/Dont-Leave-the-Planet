using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Sword,
        Gun,
        IceResource,
        FireResource,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:        return ItemData.instance.swordSprite;
            case ItemType.Gun:          return ItemData.instance.gunSprite;
            case ItemType.IceResource:  return ItemData.instance.iceResourceSprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default :
            case ItemType.IceResource:
            case ItemType.FireResource:
                return true;
            case ItemType.Gun:
            case ItemType.Sword:
                return false;
        }
    }
}
