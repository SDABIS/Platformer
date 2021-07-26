using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    [SerializeField] Transform player;

    private bool isAlive = true;
    
    [SerializeField] float aggroDistance = 5.0f;
    [SerializeField] float speed = 4.0f;
    [SerializeField] int expValue = 10;
    [SerializeField] float damage = 10;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();   
        animator = GetComponent<Animator>();

        CurrentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive) return;

        float distanceToPlayer = player.position.x - transform.position.x;
        if(Mathf.Abs(player.position.x - transform.position.x) < aggroDistance) {
            float directionToPlayer = Mathf.Sign(distanceToPlayer);
            
            if(Mathf.Abs(rb.velocity.x) < speed || Mathf.Sign(rb.velocity.x) != directionToPlayer) 
                rb.AddForce(new Vector2(speed * directionToPlayer, 0), ForceMode2D.Force);


            TurnSprite();
            animator.SetBool("isWalking", true);
        }
        else {
            animator.SetBool("isWalking", false);
        }
    }

    private void TurnSprite()
    {
        float direction = Mathf.Sign(rb.velocity.x);
        GetComponent<SpriteRenderer>().flipX = direction == -1;
    }

    public void TakeDamage(float amount) {
        CurrentHealth -= amount;
        if(CurrentHealth <= 0) Die(); 
    }

    private void Die() {

        animator.SetTrigger("isDead");
        GetComponent<Collider2D>().enabled = false;

        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        
        isAlive = false;

        characterUI.ActivateText("+" + expValue + " EXP");

        EventBroker.Instance.CallEnemyKill(this.expValue);
        ItemSpawner.Instance.SpawnItem(transform.position);

        Invoke("DestroyThis", 1);
    }

    private void DestroyThis() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer != LayerMask.NameToLayer("Player")) return;

        PlayerController pc = other.gameObject.GetComponent<PlayerController>();

        if(pc == null) return;

        pc.Hurt(damage);
    }

}
