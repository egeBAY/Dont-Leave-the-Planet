using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPf;
    public List<Item> lootList = new List<Item>();

    List<Item> GetDroppedItems()
    {
        int randomNumber = Random.Range(1, 101);
        List<Item> possibleItems = new List<Item>();
        foreach (Item item in lootList)
        {
            if(randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }

        if(possibleItems.Count > 0)
            return possibleItems;

        return null;
    }

    public void InstantiateLoots(Vector3 dropPos)
    {
        List<Item> droppedItems = GetDroppedItems();


        if (droppedItems != null)
        {
            foreach (Item dropItem in droppedItems)
            {
                ItemWorld.SpawnItemWorld(dropPos, dropItem);
                //GameObject dropped = Instantiate(droppedItemPf, dropPos, Quaternion.identity);
                // dropped.GetComponent<SpriteRenderer>().sprite = dropItem.GetSprite();
            }
        }
        else
            Debug.Log("No Item dropped");
    }
}
