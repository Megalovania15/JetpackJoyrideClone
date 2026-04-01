using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    //this class needs to spawn coins in a pattern (potentially)
    //it will also have to make sure that coins do not spawn on top of other objects
    //it will link with the object pool, and will have spawn positions and set the boundary position
    //for return to the pool
    [SerializeField] private Transform spawnPosTop, spawnPosBottom;
    [SerializeField] private Transform despawnPos;

    [SerializeField] private int minSpawnAmount, maxSpawnAmount;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float spawnDelay = 5f;

    private LinkedList<GameObject> spawnedCoins = new();

    private Coroutine spawnCoinsCoroutine;
    private Coroutine spawnDelayCoroutine;

    private ObjectPooler coinPool;

    void Start()
    {
        coinPool = GetComponent<ObjectPooler>();
        spawnDelayCoroutine = StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnCoins()
    {
        int spawnedCoinAmount = 0;
        var coinsToSpawn = DetermineNumberCoinsToSpawn();
        Vector2 spawnPos = DetermineSpawnPos();
        SpawnCoin(spawnPos);
        spawnedCoinAmount++;

        while (spawnedCoinAmount < coinsToSpawn)
        {
            //spawns the specified number of coins. Each coin's position should be adjusted by the previous
            //coin's scale
            yield return new WaitForSeconds(spawnRate);
            SpawnCoin(spawnPos);
            spawnedCoinAmount++;
        }
        
    }

    //Start the spawn coins coroutine after the delay
    IEnumerator SpawnDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            spawnCoinsCoroutine = StartCoroutine(SpawnCoins());
        }
    }

    //retrieves a coin from the coin object pool, sets its position, and enables it
    void SpawnCoin(Vector2 spawnPos)
    {
        GameObject newCoin = coinPool.RetrieveObject();
        newCoin.transform.position = spawnPos;
        var coinLevelElement = newCoin.GetComponent<LevelElement>();
        var collectible = newCoin.GetComponent<Collectible>();
        collectible.OnObjectCollect(RemoveSpawnedCoin);
        coinLevelElement.BoundaryPos = despawnPos.transform;
        newCoin.SetActive(true);
        coinLevelElement.OnBoundaryPointPassed(RemoveSpawnedCoin);
        spawnedCoins.AddLast(newCoin);
    }

    Vector2 DetermineSpawnPos()
    {
        var spawnPosY = Random.Range(spawnPosBottom.position.y, spawnPosTop.position.y);
        Vector2 newSpawnPos = new Vector2(spawnPosBottom.position.x, spawnPosY);

        while (!IsSpawnPosValid(newSpawnPos))
        {
            spawnPosY = Random.Range(spawnPosBottom.position.y, spawnPosTop.position.y);
            newSpawnPos = new Vector2(spawnPosBottom.position.x, spawnPosY);
        }
        return newSpawnPos;
    }

    bool IsSpawnPosValid(Vector2 newSpawnPos)
    {
        var coinSize = new Vector2(2f, 2f);

        if (!Physics2D.OverlapBox(newSpawnPos, coinSize, 0))
        {
            return true;
        }
        return false;
    }

    int DetermineNumberCoinsToSpawn()
    {
        int coinsToSpawn = Random.Range(minSpawnAmount, maxSpawnAmount);

        return coinsToSpawn;
    }

    void RemoveSpawnedCoin()
    {
        //take the fist value from the linked list, disable it, and remove it from the list
        while (spawnedCoins.First.Value.transform.position.x < despawnPos.position.x)
        {
            var firstCoin = spawnedCoins.First.Value;
            firstCoin.GetComponent<LevelElement>().RemoveEventListener(RemoveSpawnedCoin);
            firstCoin.GetComponent<Collectible>().RemoveEventListener(RemoveSpawnedCoin);
            coinPool.DisableObject(firstCoin);
            spawnedCoins.RemoveFirst();
        }
    }

    void RemoveSpawnedCoin(GameObject spawnedCoin)
    {
        if (spawnedCoins.Contains(spawnedCoin))
        {
            spawnedCoin.GetComponent<LevelElement>().RemoveEventListener(RemoveSpawnedCoin);
            spawnedCoin.GetComponent<Collectible>().RemoveEventListener(RemoveSpawnedCoin);
            coinPool.DisableObject(spawnedCoin);
            spawnedCoins.Remove(spawnedCoin);
        }
    }
}
