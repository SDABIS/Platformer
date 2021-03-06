using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    private bool isGrounded = false;
    private float nextJump = 0;
    private bool isFacingRight = true;
    private bool isStunned = false;

    private CharacterStats stats;
    private CharacterInventory inventory;
    public CharacterStats Stats => stats;
    public CharacterInventory Inventory => inventory;

    [SerializeField] float speed = 5.0f;
    [SerializeField] float stunTime = 1.0f;
    
    [Header("Throwables")]
    [SerializeField] int maxThrowables = 2;
    private int actualThrowables = 0;
    [SerializeField] List<Throwable> throwablePrefabsList;
    private Dictionary<string, Throwable> throwablePrefabs;
    private Throwable currentThrowable;

    [Header("Jump")]
    [SerializeField] int airJumps = 1;
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] float groundedRadius = 1.0f;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform groundCheck;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        
        throwablePrefabs = new Dictionary<string, Throwable>();

        for(int i = 0; i<throwablePrefabsList.Count; i++) {
            Throwable item = throwablePrefabsList[i];
            throwablePrefabs.Add(item.Id, item);
        }

        currentThrowable = throwablePrefabsList[0];
        EventBroker.Instance.OnEnemyKill.AddListener(HandleEnemyKill);

        stats = new CharacterStats(5, 5);
        inventory = new CharacterInventory(20);
        
    }

    private void HandleEnemyKill(int exp) {
        stats.Exp += exp;
        if(stats.HasLeveledUp) {
            characterUI.ShowLevelUp();
        }
    }

    private void FixedUpdate() {
        if(nextJump != 0) {
            rb.velocity = (new Vector2(rb.velocity.x, nextJump));
            nextJump = 0;
            animator.SetTrigger("jump");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isStunned) return;
        Move();
        Jump();
        Throw();
    }

    private void Jump()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        isGrounded = false;
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
                isGrounded = true;
                airJumps = 1;
			}
		}

        bool isJumping = Input.GetKeyDown(KeyCode.Space);

        if(isJumping && (isGrounded || airJumps > 0)) {
            if(!isGrounded) {
                airJumps--;
            }
            else {
                isGrounded = false;
            }
            nextJump = jumpForce;
            
        }
    }

    void Move() {
        
        float horizontalInput = Input.GetAxis("Horizontal");

        if(Mathf.Abs(horizontalInput) > Mathf.Epsilon) {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
            animator.SetBool("isWalking", true);

            TurnSprite();

        }

        else animator.SetBool("isWalking", false);
    }

    private void TurnSprite()
    {
        float direction = Mathf.Sign(rb.velocity.x);
        GetComponent<SpriteRenderer>().flipX = direction == -1;

        isFacingRight = direction == 1;
    }

    void Throw() {
        if(!Input.GetKeyDown(KeyCode.Z)) return;
        if(actualThrowables >= maxThrowables) return;
        Throwable newThrow = Instantiate(currentThrowable, this.transform.position, Quaternion.identity);
        newThrow.Damage += stats.Strength;
        actualThrowables++;

        if(!isFacingRight) newThrow.TurnDirection();
        newThrow.OnThrowableDestroy.AddListener(HandleThrowableDestroy);
    }

    void HandleThrowableDestroy() {
        actualThrowables--;
    }

    public void Hurt(float damage) {
        if(isStunned) return;

        float finalDamage = stats.CalculateDamage(damage);
        CurrentHealth -= finalDamage;
        
        GetStunned();
    }

    private void GetStunned() {
        float backwardsDirection = isFacingRight ? -1 : 1;
        rb.AddForce(new Vector2(backwardsDirection, 0.2f) * speed * 0.5f, ForceMode2D.Impulse);
        animator.SetTrigger("stunned");
        isStunned = true;
        Invoke("EndStun", stunTime);
    }

    private void EndStun() {
        isStunned = false;
    }

    public bool PickItem(Item item) {
        if(!inventory.AddItem(item)) return false;

        stats.EquipItem(item);

        return true;
    }
}
