using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item : MonoBehaviour
{
    private ItemPickup itemDefinition;
    [SerializeField] SpriteRenderer itemSprite;

    public ItemPickup ItemDefinition {
        get => itemDefinition;

        set {
            itemDefinition = value;

            itemSprite.sprite = ItemDefinition.sprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer != LayerMask.NameToLayer("Player")) return;

        PlayerController pc = other.gameObject.GetComponent<PlayerController>();

        if(pc == null) return;

        if(pc.PickItem(this)) gameObject.SetActive(false);

    }
}
