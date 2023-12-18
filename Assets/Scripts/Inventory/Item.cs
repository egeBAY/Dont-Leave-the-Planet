using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Sword,
        Gun,
        IceResource,
        FireResource,
        Gunpowder
    }

    public ItemType itemType;
    public int amount;
    public int dropChance;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:        return ItemData.instance.swordSprite;
            case ItemType.Gun:          return ItemData.instance.gunSprite;
            case ItemType.IceResource:  return ItemData.instance.iceResourceSprite;
            case ItemType.Gunpowder:    return ItemData.instance.gunpowderSprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default :
            case ItemType.IceResource:
            case ItemType.FireResource:
            case ItemType.Gunpowder:
                return true;
            case ItemType.Gun:
            case ItemType.Sword:
                return false;
        }
    }
}
