using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    // Start is called before the first frame update
    public void SetMaxHealth(float hp) {
        slider.maxValue = hp;
        slider.value = hp;
    }

    public void SetHealth(float hp) {
        slider.value = hp;
    }

}
