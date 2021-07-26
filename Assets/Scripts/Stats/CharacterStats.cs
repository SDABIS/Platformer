using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterStats 
{
    public enum StatType { STRENGTH, DEFENSE }

    private int _level = 1;
    private int _expToNextLevel;
    private int _exp = 0;
    private bool _hasLeveledUp = false;
    
    private Stat _strength;
    private Stat _defense;

    public float Strength {
        get { return _strength.Value; }
    }

    public float Defense {
        get { return _defense.Value; }
    }

    private Dictionary<ItemType, Item> currentEquip = new Dictionary<ItemType, Item>();
    public Dictionary<ItemType, Item> CurrentEquip => currentEquip;

    public CharacterStats(float strength, float defense) {
        _expToNextLevel = _level *= 50;

        this._strength = new Stat(strength);
        this._defense = new Stat(defense);

        foreach (ItemType i in Enum.GetValues(typeof(ItemType)))
        {
            currentEquip.Add(i, null);
        }
    }

    public int Exp {
        get {
            return _exp;
        }

        set {
            _exp = value;
            
            if(_exp > _expToNextLevel) {
                _exp -= _expToNextLevel;
                LevelUp();
            }
            else _hasLeveledUp = false;
        }
    }

    public bool HasLeveledUp => _hasLeveledUp;

    public int Level => _level;

    private void LevelUp() {
        _level++;
        _hasLeveledUp = true;

        _strength.IncreaseBase(5);
        _defense.IncreaseBase(5);

        EventBroker.Instance.CallStatChange();
    }

    public float CalculateDamage(float damage) {
        return Mathf.Clamp(damage - Defense, 0, Mathf.Infinity);
    }

    public void EquipItem(Item item) {
        ItemPickup itemDef = item.ItemDefinition;

        Item current = currentEquip[itemDef.type];

        if(current != null) UnequipItem(current);

        foreach (StatModifier mod in itemDef.modifiers)
        {
            mod.source = item;

            switch(mod.statType) {
                case StatType.STRENGTH: 
                    _strength.AddModifier(mod);
                break;

                case StatType.DEFENSE:
                    _defense.AddModifier(mod);

                break;
                default: break;
            }
        }

        currentEquip[itemDef.type] = item;

        EventBroker.Instance.CallEquipChange();
    }

    public void UnequipItem(Item item) {
        currentEquip[item.ItemDefinition.type] = null;
        _strength.RemoveAllModifiersFromSource(item);
        _defense.RemoveAllModifiersFromSource(item);
    }
}
