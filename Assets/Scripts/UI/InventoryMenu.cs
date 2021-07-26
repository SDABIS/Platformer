using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : PauseMenuBox
{
    [SerializeField] List<ItemHolder> inventoryHolders = new List<ItemHolder>();
    private int previousCount = 0;
    protected override void OnEnable() {
        updateEvent = EventBroker.Instance.OnInventoryChange;
        base.OnEnable(); 
    }

    public override void DisplayInfo()
    {
        CharacterInventory inventory = player.Inventory;

        for(int i = 0; i<inventory.Items.Count; i++) {
            inventoryHolders[i].SetSprite(inventory.Items[i].ItemDefinition.sprite);
        }

        for(int i = inventory.Items.Count; i < previousCount; i++) {
            inventoryHolders[i].SetSprite(null);
        }      

        previousCount = inventory.Items.Count;      
    }
}
