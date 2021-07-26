using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] StatMenu _statMenu;
    [SerializeField] InventoryMenu _inventoryMenu;
    [SerializeField] EquipMenu _equipMenu;
    
    private PlayerController player;


    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
        _statMenu.player = player;
        _inventoryMenu.player = player;
        _equipMenu.player = player;

        _statMenu.DisplayInfo();
        _equipMenu.DisplayInfo();
        _inventoryMenu.DisplayInfo();
    }
    
}
