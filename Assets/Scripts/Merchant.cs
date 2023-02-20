using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    protected MerchantUI merchantUI;
    protected Rigidbody2D rb;
    protected Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        merchantUI = GetComponent<MerchantUI>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
       
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;


        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        merchantUI.DisplayText();


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;


        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        merchantUI.HideText();


    }

}
