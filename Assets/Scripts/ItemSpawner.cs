using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : Singleton<ItemSpawner>
{
    [SerializeField] List<ItemPickup> itemList;
    [SerializeField] Item itemPrefab;

    [SerializeField] int poolSize = 10;
    private List<Item> itemPool = new List<Item>();
    private int spawnedItems = 0;

    void Start() {
        for(int i = 0; i<poolSize; i++) {
            Item newItem = Instantiate(itemPrefab);
            itemPool.Add(newItem);

            newItem.gameObject.SetActive(false);
        }
    }

    public void SpawnItem(Vector2 position) {
        ItemPickup itemDef = itemList[Random.Range(0, itemList.Count)];

        Item itemToSpawn = itemPool[spawnedItems];
        spawnedItems++;
        if(spawnedItems >= poolSize) spawnedItems = 0;

        itemToSpawn.ItemDefinition = itemDef;
        itemToSpawn.transform.position = position;
        itemToSpawn.gameObject.SetActive(true);
    }
}
