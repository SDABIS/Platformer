using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Throwable : MonoBehaviour
{
    [SerializeField] string id;
    public string Id => id;
    
    [SerializeField] protected Vector2 initialDirection;
    [SerializeField] float duration;
    [SerializeField] float speed;
    [SerializeField] bool destroyOnContact;
    [SerializeField] float damage = 20.0f;
    public float Damage {
        get {return damage;}
        set {damage = value;}
    }

    [HideInInspector] public UnityEvent OnThrowableDestroy;
    // Start is called before the first frame update
    void Start()
    {
        initialDirection = initialDirection.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected virtual void Move() {
        transform.Translate(initialDirection * Time.deltaTime * speed);

        duration -= Time.deltaTime;

        if(duration <= 0) Destroy(gameObject);
    }

    private void OnDestroy() {
        OnThrowableDestroy.Invoke();
    }

    public void TurnDirection() {
        transform.localScale = new Vector2( - transform.localScale.x, transform.localScale.y);

        initialDirection = new Vector2( - initialDirection.x, initialDirection.y);
    }

    protected void OnTriggerEnter2D(Collider2D other) {

        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if(enemy != null) {
            enemy.TakeDamage(damage);
        }
        if(destroyOnContact) Destroy(gameObject);
    }

}
