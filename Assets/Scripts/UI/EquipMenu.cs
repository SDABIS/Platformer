using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipMenu : PauseMenuBox
{
    [SerializeField] List<ItemHolder> _itemHoldersList = new List<ItemHolder>();
    private Dictionary<ItemType, ItemHolder> itemHolders = new Dictionary<ItemType, ItemHolder>();

    private void Awake() {
        foreach (ItemType i in Enum.GetValues(typeof(ItemType)))
        {
           itemHolders.Add(i, _itemHoldersList[(int)i]);
        }
    }

    protected override void OnEnable() {
        updateEvent = EventBroker.Instance.OnEquipChange;
        base.OnEnable(); 
    }

    public override void DisplayInfo()
    {
        Debug.Log("DISPLAY EQUIPO");
        CharacterStats stats = player.Stats;

        foreach (KeyValuePair<ItemType, Item> item in stats.CurrentEquip)
        {
            if(item.Value == null) continue;
            itemHolders[item.Key].SetSprite(item.Value.ItemDefinition.sprite);
        }
    }

}
