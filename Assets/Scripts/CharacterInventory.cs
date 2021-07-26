using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory 
{
    private List<Item> itemList = new List<Item>();
    public List<Item> Items => itemList;
    private int maxAmount;

    public CharacterInventory(int maxAmount) {
        this.maxAmount = maxAmount;
    }

    public bool AddItem(Item item) {
        if(itemList.Count >= maxAmount ) return false;

        itemList.Add(item);
        EventBroker.Instance.CallInventoryChange();
        return true;

    }

    public bool RemoveItem(Item item) {
        itemList.Remove(item);

        EventBroker.Instance.CallInventoryChange();
        return true;
    }
}
