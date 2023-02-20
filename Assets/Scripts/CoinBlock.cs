using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlock : MonoBehaviour
{
    [SerializeField] Coin coinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        if(other.contacts[0].normal.y <= 0)
        {
            return;
        }
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        pc.AddCoin(1);
        Coin newCoin = Instantiate(coinPrefab, this.transform.position, Quaternion.identity);
    }
}
