using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatMenu : PauseMenuBox
{
    [SerializeField] Text statText;
    
    protected override void OnEnable() {
        updateEvent = EventBroker.Instance.OnStatChange;
        base.OnEnable();
    }

    public override void DisplayInfo() {
        Debug.Log("DISPLAY STATS");
        CharacterStats stats = player.Stats;

        string text = "";

        text += "STRENGTH: " + stats.Strength + "\n";
        text += "DEFENSE: " + stats.Defense + "\n";

        statText.text = text;
    }

}
