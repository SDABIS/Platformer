using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected CharacterUI characterUI;
    protected Rigidbody2D rb;
    protected Animator animator;

    [SerializeField] protected float maxHealth = 100;
    protected float _currentHealth;
    protected float CurrentHealth {
        get {
            return _currentHealth;
        }
        set {
            _currentHealth = value;
            characterUI.SetHealth(_currentHealth);
        }
    }

    protected virtual void Start() {
        /*this.characterUI = GetComponent<CharacterUI>();
        characterUI.SetMaxHealth(maxHealth);
        CurrentHealth = maxHealth;*/

        this.rb = GetComponent<Rigidbody2D>();   
        this.animator = GetComponent<Animator>();  
    }
}
