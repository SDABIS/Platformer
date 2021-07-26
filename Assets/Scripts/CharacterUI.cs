using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] TempText tempText;
    [SerializeField] HealthBar healthBar;

    public void ShowLevelUp() {
        tempText.Activate("LEVEL UP!");
    }

    public void ActivateText(string text) {
        tempText.Activate(text);
    }

    public void SetMaxHealth(float amount) {
        healthBar.SetMaxHealth(amount);
    }

    public void SetHealth(float amount) {
        healthBar.SetHealth(amount);
    }
}
